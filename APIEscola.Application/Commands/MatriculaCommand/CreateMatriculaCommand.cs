using APIEscola.Domain;
using MediatR;

public class CreateMatriculaCommand : IRequest<Matricula>
{
    public Guid AlunoId { get; set; }
    public Guid TurmaId { get; set; }
    public DateTime DataMatricula { get; set; }
}