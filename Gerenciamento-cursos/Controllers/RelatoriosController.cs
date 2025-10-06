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
    public class RelatoriosController : ControllerBase
    {
        private readonly IMatriculaService _matriculaService;

        public RelatoriosController(IMatriculaService matriculaService)
        {
            // Reutilizamos o IMatriculaService, pois ele contém o método de filtro.
            _matriculaService = matriculaService;
        }

        // GET: api/relatorios/alunos-por-curso/5 - Requisito: Listar todos os alunos de um determinado curso
        [HttpGet("alunos-por-curso/{cursoId}")]
        public async Task<ActionResult<IEnumerable<AlunoModel>>> GetAlunosPorCurso(int cursoId)
        {
            var alunos = await _matriculaService.GetAlunosByCursoAsync(cursoId);

            if (alunos == null || !alunos.Any())
            {
                return NotFound($"Nenhum aluno encontrado para o Curso ID: {cursoId}.");
            }

            return Ok(alunos);
        }
    }
}
