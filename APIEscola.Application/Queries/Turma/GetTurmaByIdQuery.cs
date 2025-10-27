using APIEscola.Domain;
using MediatR;

namespace APIEscola.Application
{
    public class GetTurmaByIdQuery : IRequest<Turma>
    {
        public Guid Id { get; set; }
    }
}
