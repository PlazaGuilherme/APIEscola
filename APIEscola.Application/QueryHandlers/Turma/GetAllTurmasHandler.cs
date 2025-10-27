using APIEscola.Domain;
using APIEscola.Infrastructure;
using MediatR;

namespace APIEscola.Application
{
    public class GetAllTurmasHandler : IRequestHandler<GetAllTurmasQuery, IEnumerable<Turma>>
    {
        private readonly ITurmaRepository _repository;

        public GetAllTurmasHandler(ITurmaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Turma>> Handle(GetAllTurmasQuery request, CancellationToken cancellationToken)
        {
            return await _repository.ObterTodosComContagemDeAlunosAsync(request.PageNumber, request.PageSize);
        }
    }
}