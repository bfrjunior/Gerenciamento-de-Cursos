using Gerenciamento_cursos.Model;
using Gerenciamento_cursos.Repositories;
using Gerenciamento_cursos.Validators;

namespace Gerenciamento_cursos.Services.Aluno
{
    public class AlunoService : IAlunoService
    {
        private readonly IRepository<AlunoModel> _repository;
        private readonly IAlunoValidator _validator;

        public AlunoService(IRepository<AlunoModel> repository, IAlunoValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<IEnumerable<AlunoModel>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<AlunoModel> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<(bool Success, string ErrorMessage)> AddAsync(AlunoModel aluno)
        {
            var validationResult = await _validator.ValidateAsync(aluno, isUpdate: false);
            
            if (!validationResult.Success)
            {
                var errorMsg = validationResult.ErrorMessage ?? 
                    string.Join("; ", validationResult.Errors);
                return (false, errorMsg);
            }

            try
            {
                await _repository.AddAsync(aluno);
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao adicionar aluno: {ex.Message}");
            }
        }

        public async Task<(bool Success, string ErrorMessage)> UpdateAsync(AlunoModel aluno)
        {
            var existingAluno = await _repository.GetByIdAsync(aluno.Id);
            if (existingAluno == null)
            {
                return (false, "Aluno não encontrado.");
            }

            var validationResult = await _validator.ValidateAsync(aluno, isUpdate: true);
            
            if (!validationResult.Success)
            {
                var errorMsg = validationResult.ErrorMessage ?? 
                    string.Join("; ", validationResult.Errors);
                return (false, errorMsg);
            }

            try
            {
                // Atualiza apenas os campos necessários
                existingAluno.Nome = aluno.Nome;
                existingAluno.Email = aluno.Email;
                existingAluno.DataNascimento = aluno.DataNascimento;
                
                await _repository.UpdateAsync(existingAluno);
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao atualizar aluno: {ex.Message}");
            }
        }
    }
}

