using APIEscola.Domain;
using APIEscola.Infrastructure;
using MediatR;

namespace APIEscola.Application
{
    public class GetAlunoByIdHandler : IRequestHandler<GetAlunoByIdQuery, Aluno>
    {
        private readonly IAlunoRepository _repository;

        public GetAlunoByIdHandler(IAlunoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Aluno> Handle(GetAlunoByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.ObterPorIdAsync(request.Id);
        }
    }
}
