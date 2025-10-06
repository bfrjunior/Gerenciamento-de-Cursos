using Gerenciamento_cursos.Data;
using Gerenciamento_cursos.Dto;
using Gerenciamento_cursos.Model;
using Gerenciamento_cursos.Services.Cursos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento_cursos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly ICursoService _cursoService;

        // Injeção de ICursoService
        public CursosController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        // GET: api/cursos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CursoModel>>> GetCursos()
        {
            return Ok(await _cursoService.GetAllAsync());
        }

        // GET: api/cursos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CursoModel>> GetCurso(int id)
        {
            var curso = await _cursoService.GetByIdAsync(id);

            if (curso == null)
            {
                return NotFound();
            }

            return Ok(curso);
        }

        // POST: api/cursos
        [HttpPost]
        public async Task<ActionResult<CursoModel>> PostCurso(CursoDto cursoDto)
        {
            var curso = new CursoModel
            {
                Nome = cursoDto.Nome,
                Descricao = cursoDto.Descricao
            };

            var novoCurso = await _cursoService.AddAsync(curso);

            return CreatedAtAction(nameof(GetCurso), new { id = novoCurso.Id }, novoCurso);
        }

        // PUT: api/cursos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurso(int id, CursoDto cursoDto)
        {
            var curso = new CursoModel
            {
                Id = id,
                Nome = cursoDto.Nome,
                Descricao = cursoDto.Descricao
            };

            var success = await _cursoService.UpdateAsync(curso);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/cursos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurso(int id)
        {
            var success = await _cursoService.DeleteAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
