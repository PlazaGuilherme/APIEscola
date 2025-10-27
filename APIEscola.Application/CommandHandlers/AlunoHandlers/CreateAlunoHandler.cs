using APIEscola.Application.Validators;
using APIEscola.Domain;
using APIEscola.Infrastructure;
using MediatR;

namespace APIEscola.Application
{
    public class CreateAlunoHandler : IRequestHandler<CreateAlunoCommand, Guid>
    {
        private readonly IAlunoRepository _repositoryAluno;
        private readonly ITurmaRepository _repositoryTurma;
        private readonly IPasswordService _passwordService;

        public CreateAlunoHandler(
            IAlunoRepository repositoryAluno,
            ITurmaRepository repositoryTurma,
            IPasswordService passwordService)
        {
            _repositoryAluno = repositoryAluno;
            _repositoryTurma = repositoryTurma;
            _passwordService = passwordService;
        }

        public async Task<Guid> Handle(CreateAlunoCommand request, CancellationToken cancellationToken)
        {
            var aluno = new Aluno
            {
                Nome = request.Nome,
                Cpf = request.CPF,
                Email = request.Email,
                DataNascimento = request.DataNascimento,
                Senha = _passwordService.HashPassword(request.Senha)
            };

            if (!AlunoValidator.ValidarCamposObrigatorios(aluno))
                throw new ArgumentException("Todos os campos obrigatórios devem ser preenchidos.");

            if (!AlunoValidator.ValidarCpf(request.CPF))
                throw new ArgumentException("O CPF informado é inválido.");

            if (!AlunoValidator.ValidarEmail(request.Email))
                throw new ArgumentException("O e-mail informado é inválido.");

            if (!AlunoValidator.ValidarDataNascimento(request.DataNascimento))
                throw new ArgumentException("A data de nascimento informada é inválida.");

            var turma = await _repositoryTurma.ObterTurmaPorIdAsync(request.TurmaId);
            if (turma.Alunos.Any(a => a.Cpf == request.CPF))
                throw new InvalidOperationException("O aluno já está matriculado neste curso.");

            await _repositoryAluno.AdicionarAsync(aluno);

            return aluno.Id;
        }
    }
}