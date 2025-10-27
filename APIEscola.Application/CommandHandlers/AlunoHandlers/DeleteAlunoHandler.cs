using APIEscola.Infrastructure;
using MediatR;

namespace APIEscola.Application
{
    public class DeleteAlunoHandler : IRequestHandler<DeleteAlunoCommand, bool>
    {
        private readonly IAlunoRepository _repository;

        public DeleteAlunoHandler(IAlunoRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteAlunoCommand request, CancellationToken cancellationToken)
        {
            var aluno = await _repository.ObterPorIdAsync(request.Id);
            if (aluno == null)
                return false;

            await _repository.RemoverAsync(aluno.Id);
            return true;
        }
    }
}