using APIEscola.Domain;
using APIEscola.Infrastructure;
using MediatR;

namespace APIEscola.Application
{
    public class UpdateMatriculaHandler : IRequestHandler<UpdateMatriculaCommand, Matricula>
    {
        private readonly IMatriculaRepository _repository;

        public UpdateMatriculaHandler(IMatriculaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Matricula> Handle(UpdateMatriculaCommand request, CancellationToken cancellationToken)
        {
            var matricula = await _repository.ObterPorIdAsync(request.Id);
            if (matricula == null)
                return null;

            matricula.AlunoId = request.AlunoId;
            matricula.TurmaId = request.TurmaId;
            matricula.DataMatricula = request.DataMatricula;

            await _repository.AtualizarAsync(matricula);
            return matricula;
        }
    }
}