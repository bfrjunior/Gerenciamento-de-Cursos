using AutoMapper;
using Gerenciamento_cursos.Dto;
using Gerenciamento_cursos.Model;
using Gerenciamento_cursos.Services.Aluno;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciamento_cursos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly IAlunoService _alunoService;
        private readonly IMapper _mapper;

        public AlunosController(IAlunoService alunoService, IMapper mapper)
        {
            _alunoService = alunoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAlunos()
        {
            var result = await _alunoService.GetAllAsync();
            
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAluno(int id)
        {
            var result = await _alunoService.GetByIdAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> PostAluno(AlunoDto alunoDto)
        {
            // AutoMapper mapeia AlunoDto para AlunoModel
            var aluno = _mapper.Map<AlunoModel>(alunoDto);

            var result = await _alunoService.AddAsync(aluno);

            if (!result.Success)
                return BadRequest(result);

            return CreatedAtAction(nameof(GetAluno), new { id = result.Data.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAluno(int id, AlunoDto alunoDto)
        {
            var aluno = _mapper.Map<AlunoModel>(alunoDto);
            aluno.Id = id;

            var result = await _alunoService.UpdateAsync(aluno);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAluno(int id)
        {
            var result = await _alunoService.DeleteAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
    }
}
