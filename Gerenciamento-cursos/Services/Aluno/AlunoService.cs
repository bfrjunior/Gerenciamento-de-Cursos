using AutoMapper;
using Gerenciamento_cursos.Common.Result;
using Gerenciamento_cursos.Dto;
using Gerenciamento_cursos.Model;
using Gerenciamento_cursos.Repositories;
using Gerenciamento_cursos.Validators;

namespace Gerenciamento_cursos.Services.Aluno
{
    public class AlunoService : IAlunoService
    {
        private readonly IRepository<AlunoModel> _repository;
        private readonly IAlunoValidator _validator;
        private readonly IMapper _mapper;

        public AlunoService(IRepository<AlunoModel> repository, IAlunoValidator validator, IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<ApiResult<IEnumerable<AlunoModel>>> GetAllAsync()
        {
            try
            {
                var alunos = await _repository.GetAllAsync();
                return ApiResult<IEnumerable<AlunoModel>>.SuccessResult(alunos, "Alunos recuperados com sucesso");
            }
            catch (Exception ex)
            {
                return ApiResult<IEnumerable<AlunoModel>>.FailureResult($"Erro ao recuperar alunos: {ex.Message}");
            }
        }

        public async Task<ApiResult<AlunoModel>> GetByIdAsync(int id)
        {
            try
            {
                var aluno = await _repository.GetByIdAsync(id);
                if (aluno == null)
                    return ApiResult<AlunoModel>.FailureResult("Aluno não encontrado");

                return ApiResult<AlunoModel>.SuccessResult(aluno, "Aluno recuperado com sucesso");
            }
            catch (Exception ex)
            {
                return ApiResult<AlunoModel>.FailureResult($"Erro ao recuperar aluno: {ex.Message}");
            }
        }

        public async Task<ApiResult<AlunoModel>> AddAsync(AlunoModel aluno)
        {
            var validationResult = await _validator.ValidateAsync(aluno, isUpdate: false);

            if (!validationResult.Success)
            {
                return ApiResult<AlunoModel>.FailureResult(validationResult.Errors);
            }

            try
            {
                await _repository.AddAsync(aluno);
                return ApiResult<AlunoModel>.SuccessResult(aluno, "Aluno criado com sucesso");
            }
            catch (Exception ex)
            {
                return ApiResult<AlunoModel>.FailureResult($"Erro ao adicionar aluno: {ex.Message}");
            }
        }

        public async Task<ApiResult<AlunoModel>> UpdateAsync(AlunoModel aluno)
        {
            var existingAluno = await _repository.GetByIdAsync(aluno.Id);
            if (existingAluno == null)
            {
                return ApiResult<AlunoModel>.FailureResult("Aluno não encontrado");
            }

            var validationResult = await _validator.ValidateAsync(aluno, isUpdate: true);

            if (!validationResult.Success)
            {
                return ApiResult<AlunoModel>.FailureResult(validationResult.Errors);
            }

            try
            {
                existingAluno.Nome = aluno.Nome;
                existingAluno.Email = aluno.Email;
                existingAluno.DataNascimento = aluno.DataNascimento;

                await _repository.UpdateAsync(existingAluno);
                return ApiResult<AlunoModel>.SuccessResult(existingAluno, "Aluno atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return ApiResult<AlunoModel>.FailureResult($"Erro ao atualizar aluno: {ex.Message}");
            }
        }

        public async Task<ApiResult> DeleteAsync(int id)
        {
            try
            {
                var success = await _repository.DeleteAsync(id);
                if (!success)
                    return ApiResult.FailureResult("Aluno não encontrado");

                return ApiResult.SuccessResult("Aluno deletado com sucesso");
            }
            catch (Exception ex)
            {
                return ApiResult.FailureResult($"Erro ao deletar aluno: {ex.Message}");
            }
        }
    }
}

