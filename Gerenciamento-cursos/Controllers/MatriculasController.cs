using Gerenciamento_cursos.Data;
using Gerenciamento_cursos.Model;
using Gerenciamento_cursos.Services.Matriculas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // DTO Simples para Matrícula
        public class MatricularDto
        {
            public int AlunoId { get; set; }
            public int CursoId { get; set; }
        }

        // POST: api/matriculas
        [HttpPost]
        public async Task<IActionResult> PostMatricula(MatricularDto matricularDto)
        {
            var result = await _matriculaService.MatricularAsync(
                matricularDto.AlunoId,
                matricularDto.CursoId
            );

            if (!result.Success)
            {
                // 409 Conflict para duplicidade, 400 Bad Request para outros erros de validação
                return Conflict(result.ErrorMessage);
            }

            return Ok("Matrícula realizada com sucesso.");
        }

        // DELETE: api/matriculas?alunoId=1&cursoId=2
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
