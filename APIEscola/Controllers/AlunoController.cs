using APIEscola.Application;
using APIEscola.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIEscola.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "AdminOnly")]
    public class AlunosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AlunosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Aluno>> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetAlunoByIdQuery { Id = id });
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Aluno>> Create([FromBody] CreateAlunoCommand command)
        {
            var aluno = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = aluno }, aluno);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAlunoCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            var success = await _mediator.Send(command);
            if (success == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _mediator.Send(new DeleteAlunoCommand { Id = id });
            if (success == null)
                return NotFound();

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAllAlunos(int pageNumber = 1, int pageSize = 10)
        {
            var query = new GetAllAlunosQuery(pageNumber, pageSize);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("buscar-por-nome/{nome}")]
        public async Task<ActionResult<IEnumerable<Aluno>>> BuscarPorNome(string nome)
        {
            var query = new GetAlunoPorNomeQuery(nome);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("buscar-por-cpf/{cpf}")]
        public async Task<ActionResult<Aluno>> BuscarPorCpf(string cpf)
        {
            var query = new GetAlunoPorCpfQuery(cpf);
            var result = await _mediator.Send(query);
            
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
