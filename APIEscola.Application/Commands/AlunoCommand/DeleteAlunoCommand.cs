using MediatR;

public class DeleteAlunoCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}