using APIEscola.Domain;
using APIEscola.Infrastructure;
using MediatR;

namespace APIEscola.Application
{
    public class GetMatriculaByIdHandler : IRequestHandler<GetMatriculaByIdQuery, Matricula>
    {
        private readonly IMatriculaRepository _repository;

        public GetMatriculaByIdHandler(IMatriculaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Matricula> Handle(GetMatriculaByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.ObterPorIdAsync(request.Id);
        }
    }
}


