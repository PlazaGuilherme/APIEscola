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
    public class TurmasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TurmasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Turma>> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetTurmaByIdQuery { Id = id });
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Turma>> Create([FromBody] CreateTurmaCommand command)
        {
            var turma = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = turma.Id }, turma);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTurmaCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            var turma = await _mediator.Send(command);
            if (turma == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _mediator.Send(new DeleteTurmaCommand { Id = id });
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Turma>>> GetAllTurmas(int pageNumber = 1, int pageSize = 10)
        {
            var query = new GetAllTurmasQuery(pageNumber, pageSize);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
