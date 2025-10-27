using APIEscola.Domain;
using APIEscola.Infrastructure;
using MediatR;

namespace APIEscola.Application
{
    public class GetAllAlunosHandler : IRequestHandler<GetAllAlunosQuery, IEnumerable<Aluno>>
    {
        private readonly IAlunoRepository _repository;

        public GetAllAlunosHandler(IAlunoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Aluno>> Handle(GetAllAlunosQuery request, CancellationToken cancellationToken)
        {
            return await _repository.ObterTodosAsync(request.PageNumber, request.PageSize);
        }
    }
}