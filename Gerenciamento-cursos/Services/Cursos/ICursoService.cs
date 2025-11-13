using Gerenciamento_cursos.Model;

namespace Gerenciamento_cursos.Services.Cursos
{
    public interface ICursoService
    {
        Task<IEnumerable<CursoModel>> GetAllAsync();
        Task<CursoModel> GetByIdAsync(int id);
        Task<(bool Success, string ErrorMessage, CursoModel Curso)> AddAsync(CursoModel curso);
        Task<(bool Success, string ErrorMessage)> UpdateAsync(CursoModel curso);
        Task<bool> DeleteAsync(int id);
    }
}
