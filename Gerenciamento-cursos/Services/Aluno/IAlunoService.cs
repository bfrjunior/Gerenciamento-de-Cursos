using Gerenciamento_cursos.Common.Result;
using Gerenciamento_cursos.Model;

namespace Gerenciamento_cursos.Services.Aluno
{
    public interface IAlunoService
    {
        Task<ApiResult<IEnumerable<AlunoModel>>> GetAllAsync();
        Task<ApiResult<AlunoModel>> GetByIdAsync(int id);
        Task<ApiResult<AlunoModel>> AddAsync(AlunoModel aluno);
        Task<ApiResult<AlunoModel>> UpdateAsync(AlunoModel aluno);
        Task<ApiResult> DeleteAsync(int id);
    }
}
