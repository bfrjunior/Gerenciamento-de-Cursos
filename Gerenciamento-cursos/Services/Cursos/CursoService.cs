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

        public async Task<IEnumerable<CursoModel>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<CursoModel> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<(bool Success, string ErrorMessage, CursoModel Curso)> AddAsync(CursoModel curso)
        {
            var validationResult = _validator.Validate(curso);
            
            if (!validationResult.Success)
            {
                var errorMsg = validationResult.ErrorMessage ?? 
                    string.Join("; ", validationResult.Errors);
                return (false, errorMsg, null);
            }

            try
            {
                var newCurso = await _repository.AddAsync(curso);
                return (true, null, newCurso);
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao adicionar curso: {ex.Message}", null);
            }
        }

        public async Task<(bool Success, string ErrorMessage)> UpdateAsync(CursoModel curso)
        {
            var existingCurso = await _repository.GetByIdAsync(curso.Id);
            if (existingCurso == null)
            {
                return (false, "Curso não encontrado.");
            }

            var validationResult = _validator.Validate(curso);
            
            if (!validationResult.Success)
            {
                var errorMsg = validationResult.ErrorMessage ?? 
                    string.Join("; ", validationResult.Errors);
                return (false, errorMsg);
            }

            try
            {
                // Atualiza apenas os campos necessários
                existingCurso.Nome = curso.Nome;
                existingCurso.Descricao = curso.Descricao;
                
                await _repository.UpdateAsync(existingCurso);
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao atualizar curso: {ex.Message}");
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
