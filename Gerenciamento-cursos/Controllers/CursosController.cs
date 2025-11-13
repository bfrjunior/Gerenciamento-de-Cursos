using Gerenciamento_cursos.Dto;
using Gerenciamento_cursos.Model;
using Gerenciamento_cursos.Services.Cursos;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciamento_cursos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly ICursoService _cursoService;

        public CursosController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CursoModel>>> GetCursos()
        {
            return Ok(await _cursoService.GetAllAsync());
        }

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

        [HttpPost]
        public async Task<ActionResult<CursoModel>> PostCurso(CursoDto cursoDto)
        {
            var curso = new CursoModel
            {
                Nome = cursoDto.Nome,
                Descricao = cursoDto.Descricao
            };

            var result = await _cursoService.AddAsync(curso);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return CreatedAtAction(nameof(GetCurso), new { id = result.Curso.Id }, result.Curso);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurso(int id, CursoDto cursoDto)
        {
            var curso = new CursoModel
            {
                Id = id,
                Nome = cursoDto.Nome,
                Descricao = cursoDto.Descricao
            };

            var result = await _cursoService.UpdateAsync(curso);

            if (!result.Success)
            {
                if (result.ErrorMessage == "Curso não encontrado.")
                {
                    return NotFound();
                }
                return BadRequest(result.ErrorMessage);
            }

            return NoContent();
        }

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
