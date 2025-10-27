using APIEscola.Domain;
using MediatR;

namespace APIEscola.Application
{
    public class GetAllTurmasQuery : IRequest<IEnumerable<Turma>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public GetAllTurmasQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}