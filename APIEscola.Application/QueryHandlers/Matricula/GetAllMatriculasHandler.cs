using APIEscola.Domain;
using APIEscola.Infrastructure;
using MediatR;


namespace APIEscola.Application
{
    public class GetAllMatriculasHandler : IRequestHandler<GetAllMatriculasQuery, IEnumerable<Matricula>>
    {
        private readonly IMatriculaRepository _repository;

        public GetAllMatriculasHandler(IMatriculaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Matricula>> Handle(GetAllMatriculasQuery request, CancellationToken cancellationToken)
        {
            return await _repository.ObterTodosAsync(request.PageNumber, request.PageSize);
        }
    }
}