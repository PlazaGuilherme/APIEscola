using APIEscola.Domain;
using APIEscola.Infrastructure;
using MediatR;

namespace APIEscola.Application
{
    public class GetAlunosPorTurmaHandler : IRequestHandler<GetAlunosPorTurmaQuery, IEnumerable<Aluno>>
    {
        private readonly IAlunoRepository _repository;

        public GetAlunosPorTurmaHandler(IAlunoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Aluno>> Handle(GetAlunosPorTurmaQuery request, CancellationToken cancellationToken)
        {
            return await _repository.ObterPorTurmaIdAsync(request.TurmaId);
        }
    }
}