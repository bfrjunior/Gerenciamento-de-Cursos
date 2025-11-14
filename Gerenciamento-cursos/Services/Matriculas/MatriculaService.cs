using AutoMapper;
using Gerenciamento_cursos.Common.Result;
using Gerenciamento_cursos.Model;
using Gerenciamento_cursos.Repositories;

namespace Gerenciamento_cursos.Services.Matriculas
{
    public class MatriculaService : IMatriculaService
    {
        private readonly IRepository<AlunoModel> _alunoRepository;
        private readonly IRepository<CursoModel> _cursoRepository;
        private readonly IMatriculaRepository _matriculaRepository;
        private readonly IMapper _mapper;

        public MatriculaService(
            IRepository<AlunoModel> alunoRepository,
            IRepository<CursoModel> cursoRepository,
            IMatriculaRepository matriculaRepository,
            IMapper mapper)
        {
            _alunoRepository = alunoRepository;
            _cursoRepository = cursoRepository;
            _matriculaRepository = matriculaRepository;
            _mapper = mapper;
        }

        public async Task<ApiResult> MatricularAsync(int alunoId, int cursoId)
        {
            try
            {
                if (!await _alunoRepository.ExistsAsync(alunoId))
                    return ApiResult.FailureResult("Aluno não encontrado");

                if (!await _cursoRepository.ExistsAsync(cursoId))
                    return ApiResult.FailureResult("Curso não encontrado");

                if (await _matriculaRepository.ExistsAsync(alunoId, cursoId))
                    return ApiResult.FailureResult("O aluno já está matriculado neste curso");

                var matricula = new MatriculaModel
                {
                    AlunoId = alunoId,
                    CursoId = cursoId,
                    DataMatricula = DateTime.Now
                };

                await _matriculaRepository.AddAsync(matricula);
                return ApiResult.SuccessResult("Matrícula criada com sucesso");
            }
            catch (Exception ex)
            {
                return ApiResult.FailureResult($"Erro ao criar matrícula: {ex.Message}");
            }
        }

        public async Task<ApiResult> RemoverMatriculaAsync(int alunoId, int cursoId)
        {
            try
            {
                var success = await _matriculaRepository.DeleteAsync(alunoId, cursoId);
                if (!success)
                    return ApiResult.FailureResult("Matrícula não encontrada");

                return ApiResult.SuccessResult("Matrícula removida com sucesso");
            }
            catch (Exception ex)
            {
                return ApiResult.FailureResult($"Erro ao remover matrícula: {ex.Message}");
            }
        }

        public async Task<ApiResult<IEnumerable<AlunoModel>>> GetAlunosByCursoAsync(int cursoId)
        {
            try
            {
                var alunos = await _matriculaRepository.GetAlunosByCursoAsync(cursoId);
                return ApiResult<IEnumerable<AlunoModel>>.SuccessResult(alunos, "Alunos recuperados com sucesso");
            }
            catch (Exception ex)
            {
                return ApiResult<IEnumerable<AlunoModel>>.FailureResult($"Erro ao recuperar alunos: {ex.Message}");
            }
        }
    }
}
