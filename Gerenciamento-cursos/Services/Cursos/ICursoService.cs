using Gerenciamento_cursos.Model;

namespace Gerenciamento_cursos.Services.Cursos
{
    public interface ICursoService
    {
        Task<IEnumerable<CursoModel>> GetAllAsync();
        Task<CursoModel> GetByIdAsync(int id);
        Task<CursoModel> AddAsync(CursoModel curso);
        Task<bool> UpdateAsync(CursoModel curso);
        Task<bool> DeleteAsync(int id);
    }
}
