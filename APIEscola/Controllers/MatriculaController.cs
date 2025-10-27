using APIEscola.Application;
using APIEscola.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIEscola.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class MatriculaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MatriculaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Matricula>> Create([FromBody] CreateMatriculaCommand command)
        {
            var matricula = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = matricula.Id }, matricula);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Matricula>> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetMatriculaByIdQuery { Id = id });
            if (result == null)
                return NotFound();

            return Ok(result);
        }

  
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Matricula>>> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var query = new GetAllMatriculasQuery(pageNumber, pageSize);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMatriculaCommand command)
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
            var success = await _mediator.Send(new DeleteMatriculaCommand { Id = id });
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
