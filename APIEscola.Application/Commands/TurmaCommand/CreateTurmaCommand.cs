using APIEscola.Domain;
using MediatR;

public class CreateTurmaCommand : IRequest<Turma>
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
}