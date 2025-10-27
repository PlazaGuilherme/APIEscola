using APIEscola.Infrastructure;
using MediatR;

public class DeleteTurmaHandler : IRequestHandler<DeleteTurmaCommand, bool>
{
    private readonly AppDbContext _context;

    public DeleteTurmaHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteTurmaCommand request, CancellationToken cancellationToken)
    {
        var turma = await _context.Turmas.FindAsync(new object[] { request.Id }, cancellationToken);
        if (turma == null)
            return false;

        _context.Turmas.Remove(turma);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}