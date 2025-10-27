using APIEscola.Domain;
using APIEscola.Infrastructure;
using MediatR;

namespace APIEscola.Application
{
    public class CreateMatriculaHandler : IRequestHandler<CreateMatriculaCommand, Matricula>
    {
        private readonly IMatriculaRepository _repository;

        public CreateMatriculaHandler(IMatriculaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Matricula> Handle(CreateMatriculaCommand request, CancellationToken cancellationToken)
        {
            var matricula = new Matricula
            {
                AlunoId = request.AlunoId,
                TurmaId = request.TurmaId,
                DataMatricula = request.DataMatricula
            };

            await _repository.AdicionarAsync(matricula);
            return matricula;
        }
    }
}