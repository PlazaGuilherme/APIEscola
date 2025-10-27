using APIEscola.Domain;
using APIEscola.Infrastructure;
using MediatR;

namespace APIEscola.Application
{
    public class UpdateAlunoHandler : IRequestHandler<UpdateAlunoCommand, Aluno>
    {
        private readonly IAlunoRepository _repository;

        public UpdateAlunoHandler(IAlunoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Aluno> Handle(UpdateAlunoCommand request, CancellationToken cancellationToken)
        {
            var aluno = await _repository.ObterPorIdAsync(request.Id);
            if (aluno == null)
                return null;

            aluno.Nome = request.Nome;
            aluno.DataNascimento = request.DataNascimento;
            aluno.Cpf = request.Cpf;
            aluno.Email = request.Email;
            aluno.Senha = request.Senha;

            await _repository.AtualizarAsync(aluno);
            return aluno;
        }
    }
}