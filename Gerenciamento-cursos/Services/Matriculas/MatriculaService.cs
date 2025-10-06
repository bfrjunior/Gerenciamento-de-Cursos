using Gerenciamento_cursos.Data;
using Gerenciamento_cursos.Model;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento_cursos.Services.Matriculas
{
    public class MatriculaService : IMatriculaService
    {
        private readonly AppDbContext _context;

        public MatriculaService(AppDbContext context)
        {
            _context = context;
        }

        
        public async Task<(bool Success, string ErrorMessage)> MatricularAsync(int alunoId, int cursoId)
        {
            
            if (!await _context.Alunos.AnyAsync(a => a.Id == alunoId))
            {
                return (false, "Aluno não encontrado.");
            }
            if (!await _context.Cursos.AnyAsync(c => c.Id == cursoId))
            {
                return (false, "Curso não encontrado.");
            }

            //  Verificar se a matrícula já existe (Regra de Unicidade)
            var existeMatricula = await _context.Matriculas
                .AnyAsync(m => m.AlunoId == alunoId && m.CursoId == cursoId);

            if (existeMatricula)
            {
                return (false, "O aluno já está matriculado neste curso.");
            }

            
            var matricula = new MatriculaModel
            {
                AlunoId = alunoId,
                CursoId = cursoId,
                DataMatricula = DateTime.Now
            };

            _context.Matriculas.Add(matricula);
            await _context.SaveChangesAsync();

            return (true, null);
        }

        
        public async Task<bool> RemoverMatriculaAsync(int alunoId, int cursoId)
        {
            var matricula = await _context.Matriculas
                .FirstOrDefaultAsync(m => m.AlunoId == alunoId && m.CursoId == cursoId);

            if (matricula == null)
            {
                return false; // Matrícula não encontrada
            }

            _context.Matriculas.Remove(matricula);
            await _context.SaveChangesAsync();

            return true;
        }

        
        public async Task<IEnumerable<AlunoModel>> GetAlunosByCursoAsync(int cursoId)
        {
            // Usa o Include/Select para carregar os alunos através da tabela de junção
            var alunos = await _context.Matriculas
                .Where(m => m.CursoId == cursoId)
                .Select(m => m.Aluno)
                .Distinct()
                .ToListAsync();

            return alunos;
        }
    }
}
