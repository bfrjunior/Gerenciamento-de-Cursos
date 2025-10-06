using Gerenciamento_cursos.Data;
using Gerenciamento_cursos.Dto;
using Gerenciamento_cursos.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento_cursos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Injeção de Dependência do DBContext
        public CursosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/cursos - Requisito: Listar todos os cursos disponíveis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CursoModel>>> GetCursos()
        {
            return await _context.Cursos.ToListAsync();
        }

        // GET: api/cursos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CursoModel>> GetCurso(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);

            if (curso == null)
            {
                return NotFound();
            }

            return curso;
        }

        // POST: api/cursos - Requisito: Criar cursos
        [HttpPost]
        public async Task<ActionResult<CursoModel>> PostCurso(CursoDto cursoDto)
        {
            var curso = new CursoModel
            {
                Nome = cursoDto.Nome,
                Descricao = cursoDto.Descricao
            };

            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();

            // Retorna o curso criado com a URI para acesso direto
            return CreatedAtAction(nameof(GetCurso), new { id = curso.Id }, curso);
        }

        // PUT: api/cursos/5 - Requisito: Editar cursos
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurso(int id, CursoDto cursoDto)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }

            curso.Nome = cursoDto.Nome;
            curso.Descricao = cursoDto.Descricao;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Cursos.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent(); // Retorno padrão para atualização bem-sucedida sem conteúdo.
        }

        // DELETE: api/cursos/5 - Requisito: Excluir cursos
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurso(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
