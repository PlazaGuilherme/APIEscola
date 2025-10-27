using APIEscola.Domain;

namespace APIEscola.Infrastructure
{
    public interface ITurmaRepository
    {
        Task<Turma> AdicionarAsync(Turma turma);
        Task<Turma> AtualizarAsync(Turma turma);
        Task<bool> RemoverAsync(Guid id);
        Task<Turma> ObterPorIdAsync(Guid id);
        Task<Turma> ObterTurmaPorIdAsync(Guid id);
        Task<IEnumerable<Turma>> ObterTodosAsync();
        Task<IEnumerable<Turma>> ObterTodosAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Turma>> ObterTodosComContagemDeAlunosAsync(int pageNumber, int pageSize);

    }
}