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
    public class AlunosController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Injeção de Dependência do DBContext
        public AlunosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/alunos - Requisito: Listar todos os alunos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlunoModel>>> GetAlunos()
        {
            return await _context.Alunos.ToListAsync();
        }

        // GET: api/alunos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlunoModel>> GetAluno(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);

            if (aluno == null)
            {
                return NotFound();
            }

            return aluno;
        }

        // POST: api/alunos - Requisito: Criar alunos
        [HttpPost]
        public async Task<ActionResult<AlunoModel>> PostAluno(AlunoDto alunoDto)
        {
            // 🚨 VALIDAÇÃO DE NEGÓCIO PENDENTE:
            // Aqui deveria estar a lógica para checar se o aluno é menor de idade (DataNascimento).

            var aluno = new AlunoModel
            {
                Nome = alunoDto.Nome,
                Email = alunoDto.Email,
                DataNascimento = alunoDto.DataNascimento
            };

            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAluno), new { id = aluno.Id }, aluno);
        }

        // PUT: api/alunos/5 - Requisito: Editar alunos
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluno(int id, AlunoDto alunoDto)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }

            // Atualiza as propriedades
            aluno.Nome = alunoDto.Nome;
            aluno.Email = alunoDto.Email;
            aluno.DataNascimento = alunoDto.DataNascimento;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Alunos.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/alunos/5 - Requisito: Excluir alunos
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
