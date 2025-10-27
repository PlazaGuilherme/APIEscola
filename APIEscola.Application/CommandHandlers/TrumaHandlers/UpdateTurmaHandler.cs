using APIEscola.Domain;
using APIEscola.Infrastructure;
using MediatR;

public class UpdateTurmaHandler : IRequestHandler<UpdateTurmaCommand, Turma>
{
    private readonly ITurmaRepository _repository;

    public UpdateTurmaHandler(ITurmaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Turma> Handle(UpdateTurmaCommand request, CancellationToken cancellationToken)
    {
        var turma = await _repository.ObterPorIdAsync(request.Id);
        if (turma == null)
            return null;

        turma.Nome = request.Nome;
        turma.Descricao = request.Descricao;

        await _repository.AtualizarAsync(turma);
        return turma;
    }
}