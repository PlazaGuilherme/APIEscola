using APIEscola.Domain;
using Microsoft.EntityFrameworkCore;

namespace APIEscola.Infrastructure
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly AppDbContext _context;

        public AlunoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Aluno> AdicionarAsync(Aluno aluno)
        {
            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();
            return aluno;
        }

        public async Task<Aluno> AtualizarAsync(Aluno aluno)
        {
            _context.Alunos.Update(aluno);
            await _context.SaveChangesAsync();
            return aluno;
        }
        public async Task<bool> RemoverAsync(Guid id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null)
                return false;

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Aluno> ObterPorIdAsync(Guid id)
        {
            return await _context.Alunos
                .Include(a => a.Matriculas)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<IEnumerable<Aluno>> ObterTodosAsync()
        {
            return await _context.Alunos
                .Include(a => a.Matriculas)
                .ToListAsync();
        }

        public async Task<IEnumerable<Aluno>> ObterPorTurmaIdAsync(Guid turmaId)
        {
            return await _context.Alunos
                .Include(a => a.Matriculas)
                .Where(a => a.Matriculas.Any(m => m.TurmaId == turmaId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Aluno>> ObterTodosAsync(int pageNumber, int pageSize)
        {
            return await _context.Alunos
                .Include(a => a.Matriculas)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Aluno>> BuscarPorNomeAsync(string nome)
        {
            return await _context.Alunos
                .Include(a => a.Matriculas)
                .Where(a => a.Nome.Contains(nome))
                .ToListAsync();
        }

        public async Task<Aluno> BuscarPorCpfAsync(string cpf)
        {
            return await _context.Alunos
                .Include(a => a.Matriculas)
                .FirstOrDefaultAsync(a => a.Cpf == cpf);
        }
    }
}
