using Gerenciamento_cursos.Common.Result;
using Gerenciamento_cursos.Model;

namespace Gerenciamento_cursos.Services.Matriculas
{
    public interface IMatriculaService
    {
        Task<ApiResult> MatricularAsync(int alunoId, int cursoId);
        Task<ApiResult> RemoverMatriculaAsync(int alunoId, int cursoId);
        Task<ApiResult<IEnumerable<AlunoModel>>> GetAlunosByCursoAsync(int cursoId);
    }
}
