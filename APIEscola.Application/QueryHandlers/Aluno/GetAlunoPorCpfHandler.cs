using APIEscola.Domain;
using APIEscola.Infrastructure;
using MediatR;

namespace APIEscola.Application
{
    public class GetAlunoPorCpfHandler : IRequestHandler<GetAlunoPorCpfQuery, Aluno>
    {
        private readonly IAlunoRepository _repository;

        public GetAlunoPorCpfHandler(IAlunoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Aluno> Handle(GetAlunoPorCpfQuery request, CancellationToken cancellationToken)
        {
            return await _repository.BuscarPorCpfAsync(request.Cpf);
        }
    }
}


