using Gerenciamento_cursos.Model;

namespace Gerenciamento_cursos.Services.Aluno
{
    public interface IAlunoService
    {
        Task<IEnumerable<AlunoModel>> GetAllAsync();
        Task<AlunoModel> GetByIdAsync(int id);
        Task<(bool Success, string ErrorMessage)> AddAsync(AlunoModel aluno);
        Task<(bool Success, string ErrorMessage)> UpdateAsync(AlunoModel aluno);
        Task<bool> DeleteAsync(int id);
    }
}
