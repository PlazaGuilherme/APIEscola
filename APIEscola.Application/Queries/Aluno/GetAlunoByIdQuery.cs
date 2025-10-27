using MediatR;
using APIEscola.Domain;

public class GetAlunoByIdQuery : IRequest<Aluno>
{
    public Guid Id { get; set; }
}
