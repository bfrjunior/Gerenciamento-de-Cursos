using Gerenciamento_cursos.Common.Result;
using Gerenciamento_cursos.Model;

namespace Gerenciamento_cursos.Services.Cursos
{
    public interface ICursoService
    {
        Task<ApiResult<IEnumerable<CursoModel>>> GetAllAsync();
        Task<ApiResult<CursoModel>> GetByIdAsync(int id);
        Task<ApiResult<CursoModel>> AddAsync(CursoModel curso);
        Task<ApiResult<CursoModel>> UpdateAsync(CursoModel curso);
        Task<ApiResult> DeleteAsync(int id);
    }
}
