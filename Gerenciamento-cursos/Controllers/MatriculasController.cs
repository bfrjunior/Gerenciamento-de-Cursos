using Gerenciamento_cursos.Data;
using Gerenciamento_cursos.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento_cursos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatriculasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MatriculasController(AppDbContext context)
        {
            _context = context;
        }

        // DTO Simples para Matrícula (Poderia estar em DTOs)
        public class MatricularDto
        {
            public int AlunoId { get; set; }
            public int CursoId { get; set; }
        }

        // POST: api/matriculas - Requisito: Possibilidade de matricular um aluno em um curso
        [HttpPost]
        public async Task<IActionResult> PostMatricula(MatricularDto matricularDto)
        {
            // 1. Verificar se a matrícula já existe
            var existeMatricula = await _context.Matriculas
                .AnyAsync(m => m.AlunoId == matricularDto.AlunoId && m.CursoId == matricularDto.CursoId);

            if (existeMatricula)
            {
                return Conflict("O aluno já está matriculado neste curso.");
            }

            // 2. Criar nova matrícula
            var matricula = new MatriculaModel
            {
                AlunoId = matricularDto.AlunoId,
                CursoId = matricularDto.CursoId,
                DataMatricula = DateTime.Now
            };

            _context.Matriculas.Add(matricula);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostMatricula), matricula);
        }

        // DELETE: api/matriculas?alunoId=1&cursoId=2 - Requisito: Remover um aluno de um curso
        [HttpDelete]
        public async Task<IActionResult> DeleteMatricula([FromQuery] int alunoId, [FromQuery] int cursoId)
        {
            var matricula = await _context.Matriculas
                .FirstOrDefaultAsync(m => m.AlunoId == alunoId && m.CursoId == cursoId);

            if (matricula == null)
            {
                return NotFound("Matrícula não encontrada.");
            }

            _context.Matriculas.Remove(matricula);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
