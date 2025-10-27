using MediatR;
using APIEscola.Domain;

public class UpdateMatriculaCommand : IRequest<Matricula>
{
    public Guid Id { get; set; }
    public Guid AlunoId { get; set; }
    public Guid TurmaId { get; set; }
    public DateTime DataMatricula { get; set; }
}
