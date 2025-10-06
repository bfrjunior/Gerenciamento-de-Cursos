using Gerenciamento_cursos.Model;

namespace Gerenciamento_cursos.Services.Matriculas
{
    public interface IMatriculaService
    {
        Task<(bool Success, string ErrorMessage)> MatricularAsync(int alunoId, int cursoId);
        Task<bool> RemoverMatriculaAsync(int alunoId, int cursoId);
        Task<IEnumerable<AlunoModel>> GetAlunosByCursoAsync(int cursoId);
    }
}
