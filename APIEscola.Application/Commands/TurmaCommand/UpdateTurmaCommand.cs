using MediatR;
using APIEscola.Domain;

public class UpdateTurmaCommand : IRequest<Turma>
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
}
