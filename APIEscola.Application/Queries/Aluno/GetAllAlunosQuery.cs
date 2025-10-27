using APIEscola.Domain;
using MediatR;

namespace APIEscola.Application
{
    public class GetAllAlunosQuery : IRequest<IEnumerable<Aluno>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public GetAllAlunosQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}