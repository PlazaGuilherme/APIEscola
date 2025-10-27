using APIEscola.Domain;
using Microsoft.EntityFrameworkCore;

namespace APIEscola.Infrastructure
{
    public class TurmaRepository : ITurmaRepository
    {
        private readonly AppDbContext _context;

        public TurmaRepository(AppDbContext context)
        {
            _context = context;
        }

        // Adicionar uma nova turma
        public async Task<Turma> AdicionarAsync(Turma turma)
        {
            _context.Turmas.Add(turma);
            await _context.SaveChangesAsync();
            return turma;
        }

        // Atualizar uma turma existente
        public async Task<Turma> AtualizarAsync(Turma turma)
        {
            _context.Turmas.Update(turma);
            await _context.SaveChangesAsync();
            return turma;
        }

        public async Task<bool> RemoverAsync(Guid id)
        {
            var turma = await _context.Turmas.FindAsync(id);
            if (turma == null)
                return false;

            _context.Turmas.Remove(turma);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Turma> ObterPorIdAsync(Guid id)
        {
            return await _context.Turmas
                .Include(t => t.Matriculas)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Turma> ObterTurmaPorIdAsync(Guid id)
        {
            return await ObterPorIdAsync(id);
        }

        // Obter todas as turmas
        public async Task<IEnumerable<Turma>> ObterTodosAsync()
        {
            return await _context.Turmas
                .Include(t => t.Matriculas)
                .ToListAsync();
        }

        public async Task<IEnumerable<Turma>> ObterTodosAsync(int pageNumber, int pageSize)
        {
            return await _context.Turmas
                .Include(t => t.Matriculas)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Turma>> ObterTodosComContagemDeAlunosAsync(int pageNumber, int pageSize)
        {
            return await _context.Turmas
                .Include(t => t.Matriculas)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}