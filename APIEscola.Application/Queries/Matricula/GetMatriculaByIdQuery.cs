using APIEscola.Domain;
using MediatR;

namespace APIEscola.Application
{
    public class GetMatriculaByIdQuery : IRequest<Matricula>
    {
        public Guid Id { get; set; }

    }
}