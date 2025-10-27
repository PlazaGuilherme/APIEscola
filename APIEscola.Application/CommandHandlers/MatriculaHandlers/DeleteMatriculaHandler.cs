using APIEscola.Infrastructure;
using MediatR;

namespace APIEscola.Application
{
    public class DeleteMatriculaHandler : IRequestHandler<DeleteMatriculaCommand, bool>
    {
        private readonly IMatriculaRepository _repository;

        public DeleteMatriculaHandler(IMatriculaRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteMatriculaCommand request, CancellationToken cancellationToken)
        {
            var matricula = await _repository.ObterPorIdAsync(request.Id);
            if (matricula == null)
                return false;

            await _repository.RemoverAsync(matricula.Id);
            return true;
        }
    }
}
