using MediatR;

public class DeleteMatriculaCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}