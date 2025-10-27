using APIEscola.Domain;
using MediatR;

namespace APIEscola.Application
{
    public class GetAllMatriculasQuery : IRequest<IEnumerable<Matricula>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public GetAllMatriculasQuery(int pageNumber = 1, int pageSize = 10)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
