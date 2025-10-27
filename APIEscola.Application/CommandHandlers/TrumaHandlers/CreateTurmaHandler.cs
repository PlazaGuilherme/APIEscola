using APIEscola.Application;
using APIEscola.Domain;
using APIEscola.Infrastructure;
using MediatR;

public class CreateTurmaHandler : IRequestHandler<CreateTurmaCommand, Turma>
{
    private readonly ITurmaRepository _repository;

    public CreateTurmaHandler(ITurmaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Turma> Handle(CreateTurmaCommand request, CancellationToken cancellationToken)
    {
        if (!TurmaValidator.ValidarNome(request.Nome))
            throw new ArgumentException("O nome da turma deve ter entre 3 e 100 caracteres.");

        if (!TurmaValidator.ValidarDescricao(request.Descricao))
            throw new ArgumentException("A descrição da turma deve ter entre 10 e 250 caracteres.");

        var turma = new Turma
        {
            Nome = request.Nome,
            Descricao = request.Descricao
        };

        await _repository.AdicionarAsync(turma);
        return turma;
    }
}