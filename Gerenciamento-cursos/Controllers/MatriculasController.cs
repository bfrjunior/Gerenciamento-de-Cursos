using Gerenciamento_cursos.Dto;
using Gerenciamento_cursos.Services.Matriculas;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciamento_cursos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatriculasController : ControllerBase
    {
        private readonly IMatriculaService _matriculaService;

        public MatriculasController(IMatriculaService matriculaService)
        {
            _matriculaService = matriculaService;
        }

        [HttpPost]
        public async Task<ActionResult> PostMatricula(MatricularDto matricularDto)
        {
            var result = await _matriculaService.MatricularAsync(
                matricularDto.AlunoId,
                matricularDto.CursoId
            );

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteMatricula([FromQuery] int alunoId, [FromQuery] int cursoId)
        {
            var result = await _matriculaService.RemoverMatriculaAsync(alunoId, cursoId);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet("curso/{cursoId}")]
        public async Task<ActionResult> GetAlunosByCurso(int cursoId)
        {
            var result = await _matriculaService.GetAlunosByCursoAsync(cursoId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
