using Microsoft.AspNetCore.Identity;

namespace APIEscola.Domain
{
    public class Usuario : IdentityUser
    {
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public bool IsAdmin { get; set; }
    }
}
