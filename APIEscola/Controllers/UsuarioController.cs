using APIEscola.Domain;
using APIEscola.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace APIEscola.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtService _jwtService;

        public UsuarioController(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            RoleManager<IdentityRole> roleManager,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
        }

        [HttpPost("registro")]
        [AllowAnonymous]
        public async Task<IActionResult> Registro([FromBody] RegistroModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new Usuario
            {
                UserName = model.Email,
                Email = model.Email,
                Nome = model.Nome,
                Cpf = model.Cpf,
                DataNascimento = model.DataNascimento,
                IsAdmin = model.IsAdmin
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new IdentityRole("Admin"));

            if (model.IsAdmin)
                await _userManager.AddToRoleAsync(user, "Admin");

            return Ok(new { message = "Usuário registrado com sucesso", userId = user.Id });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            
            if (user == null)
                return Unauthorized(new { message = "Email ou senha inválidos" });

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
                return Unauthorized(new { message = "Email ou senha inválidos" });

            if (!user.IsAdmin)
                return Forbid("Apenas administradores podem acessar o sistema");

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtService.GenerateToken(user);

            return Ok(new 
            { 
                message = "Login realizado com sucesso",
                token = token,
                userId = user.Id,
                email = user.Email,
                nome = user.Nome,
                isAdmin = user.IsAdmin,
                roles = roles
            });
        }

        [HttpGet("logout")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Logout realizado com sucesso" });
        }

        [HttpGet("usuario-atual")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetUsuarioAtual()
        {
            var user = await _userManager.GetUserAsync(User);
            
            if (user == null)
                return Unauthorized();

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new
            {
                id = user.Id,
                email = user.Email,
                nome = user.Nome,
                cpf = user.Cpf,
                dataNascimento = user.DataNascimento,
                isAdmin = user.IsAdmin,
                roles = roles
            });
        }
    }

    public class RegistroModel
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; } = false;
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
