using MediatR;

public class DeleteTurmaCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}