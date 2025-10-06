using Gerenciamento_cursos.Data;
using Gerenciamento_cursos.Dto;
using Gerenciamento_cursos.Model;
using Gerenciamento_cursos.Services.Aluno;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento_cursos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {

        private readonly IAlunoService _alunoService;

        public AlunosController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlunoModel>>> GetAlunos()
        {

            return Ok(await _alunoService.GetAllAsync());
        }

        // GET: api/alunos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlunoModel>> GetAluno(int id)
        {
            var aluno = await _alunoService.GetByIdAsync(id);

            if (aluno == null)
            {
                return NotFound();
            }

            return Ok(aluno);
        }


        [HttpPost]
        public async Task<ActionResult<AlunoModel>> PostAluno(AlunoDto alunoDto)
        {

            var aluno = new AlunoModel
            {
                Nome = alunoDto.Nome,
                Email = alunoDto.Email,
                DataNascimento = alunoDto.DataNascimento
            };


            var result = await _alunoService.AddAsync(aluno);

            if (!result.Success)
            {

                return BadRequest(result.ErrorMessage);
            }


            return CreatedAtAction(nameof(GetAluno), new { id = aluno.Id }, aluno);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluno(int id, AlunoDto alunoDto)
        {

            var aluno = new AlunoModel
            {
                Id = id,
                Nome = alunoDto.Nome,
                Email = alunoDto.Email,
                DataNascimento = alunoDto.DataNascimento
            };

            var result = await _alunoService.UpdateAsync(aluno);

            if (!result.Success)
            {
                if (result.ErrorMessage == "Aluno não encontrado.")
                {
                    return NotFound();
                }

                return BadRequest(result.ErrorMessage);
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno(int id)
        {
            var deleted = await _alunoService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
