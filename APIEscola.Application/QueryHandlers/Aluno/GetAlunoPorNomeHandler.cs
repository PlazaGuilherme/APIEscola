using APIEscola.Domain;
using APIEscola.Infrastructure;
using MediatR;

namespace APIEscola.Application
{
    public class GetAlunoPorNomeHandler : IRequestHandler<GetAlunoPorNomeQuery, IEnumerable<Aluno>>
    {
        private readonly IAlunoRepository _repository;

        public GetAlunoPorNomeHandler(IAlunoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Aluno>> Handle(GetAlunoPorNomeQuery request, CancellationToken cancellationToken)
        {
            return await _repository.BuscarPorNomeAsync(request.Nome);
        }
    }
}


