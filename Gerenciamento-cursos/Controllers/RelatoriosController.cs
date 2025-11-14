using Gerenciamento_cursos.Model;
using Gerenciamento_cursos.Services.Matriculas;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciamento_cursos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatoriosController : ControllerBase
    {
        private readonly IMatriculaService _matriculaService;

        public RelatoriosController(IMatriculaService matriculaService)
        {
            _matriculaService = matriculaService;
        }

        [HttpGet("alunos-por-curso/{cursoId}")]
        public async Task<ActionResult> GetAlunosPorCurso(int cursoId)
        {
            var resultado = await _matriculaService.GetAlunosByCursoAsync(cursoId);

            if (!resultado.Success)
                return BadRequest(resultado);

            if (resultado.Data == null || !resultado.Data.Any())
                return NotFound(new { message = $"Nenhum aluno encontrado para o Curso ID: {cursoId}." });

            return Ok(resultado);
        }
    }
}
