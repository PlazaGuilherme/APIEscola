using APIEscola.Domain;
using Microsoft.EntityFrameworkCore;

namespace APIEscola.Infrastructure
{
    public class MatriculaRepository : IMatriculaRepository
    {
        private readonly AppDbContext _context;

        public MatriculaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Matricula> AdicionarAsync(Matricula matricula)
        {
            _context.Matriculas.Add(matricula);
            await _context.SaveChangesAsync();
            return matricula;
        }

        public async Task<Matricula> AtualizarAsync(Matricula matricula)
        {
            _context.Matriculas.Update(matricula);
            await _context.SaveChangesAsync();
            return matricula;
        }

        public async Task<bool> RemoverAsync(Guid id)
        {
            var matricula = await _context.Matriculas.FindAsync(id);
            if (matricula == null)
                return false;

            _context.Matriculas.Remove(matricula);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Matricula> ObterPorIdAsync(Guid id)   
        {
            return await _context.Matriculas
                .Include(m => m.Aluno)
                .Include(m => m.Turma)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Matricula>> ObterTodosAsync(int pageNumber = 1, int pageSize = 10)
        {
            return await _context.Matriculas
                .Include(m => m.Aluno)
                .Include(m => m.Turma)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}