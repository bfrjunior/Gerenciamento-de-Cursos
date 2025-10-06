using Gerenciamento_cursos.Data;
using Gerenciamento_cursos.Model;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento_cursos.Services.Cursos
{
    public class CursoService : ICursoService
    {
        private readonly AppDbContext _context;

        public CursoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CursoModel>> GetAllAsync() => await _context.Cursos.ToListAsync();
        public async Task<CursoModel> GetByIdAsync(int id) => await _context.Cursos.FindAsync(id);

        public async Task<CursoModel> AddAsync(CursoModel curso)
        {
            
            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();
            return curso;
        }

        public async Task<bool> UpdateAsync(CursoModel curso)
        {
            var existingCurso = await _context.Cursos.FindAsync(curso.Id);
            if (existingCurso == null) return false;

            
            existingCurso.Nome = curso.Nome;
            existingCurso.Descricao = curso.Descricao;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Cursos.Any(e => e.Id == curso.Id))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null) return false;

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
