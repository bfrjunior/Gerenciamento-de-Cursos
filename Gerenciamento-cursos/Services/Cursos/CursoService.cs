using AutoMapper;
using Gerenciamento_cursos.Common.Result;
using Gerenciamento_cursos.Model;
using Gerenciamento_cursos.Repositories;
using Gerenciamento_cursos.Validators;

namespace Gerenciamento_cursos.Services.Cursos
{
    public class CursoService : ICursoService
    {
        private readonly IRepository<CursoModel> _repository;
        private readonly ICursoValidator _validator;

        public CursoService(IRepository<CursoModel> repository, ICursoValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<ApiResult<IEnumerable<CursoModel>>> GetAllAsync()
        {
            try
            {
                var cursos = await _repository.GetAllAsync();
                return ApiResult<IEnumerable<CursoModel>>.SuccessResult(cursos, "Cursos recuperados com sucesso");
            }
            catch (Exception ex)
            {
                return ApiResult<IEnumerable<CursoModel>>.FailureResult($"Erro ao recuperar cursos: {ex.Message}");
            }
        }

        public async Task<ApiResult<CursoModel>> GetByIdAsync(int id)
        {
            try
            {
                var curso = await _repository.GetByIdAsync(id);
                if (curso == null)
                    return ApiResult<CursoModel>.FailureResult("Curso não encontrado");

                return ApiResult<CursoModel>.SuccessResult(curso, "Curso recuperado com sucesso");
            }
            catch (Exception ex)
            {
                return ApiResult<CursoModel>.FailureResult($"Erro ao recuperar curso: {ex.Message}");
            }
        }

        public async Task<ApiResult<CursoModel>> AddAsync(CursoModel curso)
        {
            var validationResult = _validator.Validate(curso);
            
            if (!validationResult.Success)
            {
                var errorMsg = validationResult.ErrorMessage ?? 
                    string.Join("; ", validationResult.Errors);
                return ApiResult<CursoModel>.FailureResult(errorMsg);
            }

            try
            {
                var newCurso = await _repository.AddAsync(curso);
                return ApiResult<CursoModel>.SuccessResult(newCurso, "Curso criado com sucesso");
            }
            catch (Exception ex)
            {
                return ApiResult<CursoModel>.FailureResult($"Erro ao adicionar curso: {ex.Message}");
            }
        }

        public async Task<ApiResult<CursoModel>> UpdateAsync(CursoModel curso)
        {
            var existingCurso = await _repository.GetByIdAsync(curso.Id);
            if (existingCurso == null)
            {
                return ApiResult<CursoModel>.FailureResult("Curso não encontrado");
            }

            var validationResult = _validator.Validate(curso);
            
            if (!validationResult.Success)
            {
                var errorMsg = validationResult.ErrorMessage ?? 
                    string.Join("; ", validationResult.Errors);
                return ApiResult<CursoModel>.FailureResult(errorMsg);
            }

            try
            {
                existingCurso.Nome = curso.Nome;
                existingCurso.Descricao = curso.Descricao;
                
                await _repository.UpdateAsync(existingCurso);
                return ApiResult<CursoModel>.SuccessResult(existingCurso, "Curso atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return ApiResult<CursoModel>.FailureResult($"Erro ao atualizar curso: {ex.Message}");
            }
        }

        public async Task<ApiResult> DeleteAsync(int id)
        {
            try
            {
                var success = await _repository.DeleteAsync(id);
                if (!success)
                    return ApiResult.FailureResult("Curso não encontrado");

                return ApiResult.SuccessResult("Curso excluído com sucesso");
            }
            catch (Exception ex)
            {
                return ApiResult.FailureResult($"Erro ao excluir curso: {ex.Message}");
            }
        }
    }
}
