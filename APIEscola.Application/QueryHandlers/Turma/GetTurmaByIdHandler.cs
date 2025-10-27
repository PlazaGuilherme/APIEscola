using APIEscola.Domain;
using APIEscola.Infrastructure; 
using MediatR;

namespace APIEscola.Application
{
    public class GetTurmaByIdHandler : IRequestHandler<GetTurmaByIdQuery, Turma>
    {
        private readonly ITurmaRepository _repository;

        public GetTurmaByIdHandler(ITurmaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Turma> Handle(GetTurmaByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.ObterPorIdAsync(request.Id);
        }
    }
}