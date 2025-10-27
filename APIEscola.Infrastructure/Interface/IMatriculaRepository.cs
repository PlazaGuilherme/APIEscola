using APIEscola.Domain;

namespace APIEscola.Infrastructure
{
    public interface IMatriculaRepository
    {
        Task<Matricula> AdicionarAsync(Matricula matricula);
        Task<Matricula> AtualizarAsync(Matricula matricula);
        Task<bool> RemoverAsync(Guid id);
        Task<Matricula> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Matricula>> ObterTodosAsync(int pageNumber = 1, int pageSize = 10);
    }
}