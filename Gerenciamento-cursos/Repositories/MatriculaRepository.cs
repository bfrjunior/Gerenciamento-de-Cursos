using Gerenciamento_cursos.Data;
using Gerenciamento_cursos.Model;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento_cursos.Repositories
{
    public class MatriculaRepository : IMatriculaRepository
    {
        private readonly AppDbContext _context;

        public MatriculaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MatriculaModel>> GetAllAsync()
        {
            return await _context.Matriculas
                .Include(m => m.Aluno)
                .Include(m => m.Curso)
                .ToListAsync();
        }

        public async Task<MatriculaModel> GetByKeysAsync(int alunoId, int cursoId)
        {
            return await _context.Matriculas
                .Include(m => m.Aluno)
                .Include(m => m.Curso)
                .FirstOrDefaultAsync(m => m.AlunoId == alunoId && m.CursoId == cursoId);
        }

        public async Task<MatriculaModel> AddAsync(MatriculaModel matricula)
        {
            await _context.Matriculas.AddAsync(matricula);
            await _context.SaveChangesAsync();
            return matricula;
        }

        public async Task<bool> DeleteAsync(int alunoId, int cursoId)
        {
            var matricula = await GetByKeysAsync(alunoId, cursoId);
            if (matricula == null) return false;

            _context.Matriculas.Remove(matricula);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int alunoId, int cursoId)
        {
            return await _context.Matriculas
                .AnyAsync(m => m.AlunoId == alunoId && m.CursoId == cursoId);
        }

        public async Task<IEnumerable<AlunoModel>> GetAlunosByCursoAsync(int cursoId)
        {
            return await _context.Matriculas
                .Where(m => m.CursoId == cursoId)
                .Include(m => m.Aluno)
                .Select(m => m.Aluno)
                .Distinct()
                .ToListAsync();
        }
    }
}