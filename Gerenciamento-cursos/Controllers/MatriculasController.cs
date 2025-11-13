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
        public async Task<IActionResult> PostMatricula(MatricularDto matricularDto)
        {
            var result = await _matriculaService.MatricularAsync(
                matricularDto.AlunoId,
                matricularDto.CursoId
            );

            if (!result.Success)
            {
                return Conflict(result.ErrorMessage);
            }

            return Ok("Matrícula realizada com sucesso.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMatricula([FromQuery] int alunoId, [FromQuery] int cursoId)
        {
            var success = await _matriculaService.RemoverMatriculaAsync(alunoId, cursoId);

            if (!success)
            {
                return NotFound("Matrícula não encontrada.");
            }

            return NoContent();
        }
    }
}
