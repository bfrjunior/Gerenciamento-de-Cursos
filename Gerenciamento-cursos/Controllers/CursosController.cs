using AutoMapper;
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
        private readonly IMapper _mapper;

        public CursosController(ICursoService cursoService, IMapper mapper)
        {
            _cursoService = cursoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetCursos()
        {
            var result = await _cursoService.GetAllAsync();

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCurso(int id)
        {
            var result = await _cursoService.GetByIdAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> PostCurso(CursoDto cursoDto)
        {
            var curso = _mapper.Map<CursoModel>(cursoDto);

            var result = await _cursoService.AddAsync(curso);

            if (!result.Success)
                return BadRequest(result);

            return CreatedAtAction(nameof(GetCurso), new { id = result.Data.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutCurso(int id, CursoDto cursoDto)
        {
            var curso = _mapper.Map<CursoModel>(cursoDto);
            curso.Id = id;

            var result = await _cursoService.UpdateAsync(curso);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCurso(int id)
        {
            var result = await _cursoService.DeleteAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
    }
}
