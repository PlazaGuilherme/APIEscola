using APIEscola.Domain;

namespace APIEscola.Infrastructure
{
    public interface IAlunoRepository
    {
        Task<Aluno> AdicionarAsync(Aluno aluno);
        Task<Aluno> AtualizarAsync(Aluno aluno);
        Task<bool> RemoverAsync(Guid id);
        Task<Aluno> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Aluno>> ObterTodosAsync();
        Task<IEnumerable<Aluno>> ObterPorTurmaIdAsync(Guid turmaId);
        Task<IEnumerable<Aluno>> ObterTodosAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Aluno>> BuscarPorNomeAsync(string nome);
        Task<Aluno> BuscarPorCpfAsync(string cpf);
    }
}