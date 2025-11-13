using Gerenciamento_cursos.Model;

namespace Gerenciamento_cursos.Repositories
{
    public interface IMatriculaRepository
    {
        Task<IEnumerable<MatriculaModel>> GetAllAsync();
        Task<MatriculaModel> GetByKeysAsync(int alunoId, int cursoId);
        Task<MatriculaModel> AddAsync(MatriculaModel matricula);
        Task<bool> DeleteAsync(int alunoId, int cursoId);
        Task<bool> ExistsAsync(int alunoId, int cursoId);
        Task<IEnumerable<AlunoModel>> GetAlunosByCursoAsync(int cursoId);
    }
}