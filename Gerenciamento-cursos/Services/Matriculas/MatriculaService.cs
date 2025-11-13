using Gerenciamento_cursos.Model;
using Gerenciamento_cursos.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento_cursos.Services.Matriculas
{
    public class MatriculaService : IMatriculaService
    {
        private readonly IRepository<AlunoModel> _alunoRepository;
        private readonly IRepository<CursoModel> _cursoRepository;
        private readonly IMatriculaRepository _matriculaRepository;

        public MatriculaService(
            IRepository<AlunoModel> alunoRepository,
            IRepository<CursoModel> cursoRepository,
            IMatriculaRepository matriculaRepository)
        {
            _alunoRepository = alunoRepository;
            _cursoRepository = cursoRepository;
            _matriculaRepository = matriculaRepository;
        }

        public async Task<(bool Success, string ErrorMessage)> MatricularAsync(int alunoId, int cursoId)
        {
            if (!await _alunoRepository.ExistsAsync(alunoId))
            {
                return (false, "Aluno não encontrado.");
            }

            if (!await _cursoRepository.ExistsAsync(cursoId))
            {
                return (false, "Curso não encontrado.");
            }

            if (await _matriculaRepository.ExistsAsync(alunoId, cursoId))
            {
                return (false, "O aluno já está matriculado neste curso.");
            }

            var matricula = new MatriculaModel
            {
                AlunoId = alunoId,
                CursoId = cursoId,
                DataMatricula = DateTime.Now
            };

            try
            {
                await _matriculaRepository.AddAsync(matricula);
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"Erro ao criar matrícula: {ex.Message}");
            }
        }

        public async Task<bool> RemoverMatriculaAsync(int alunoId, int cursoId)
        {
            try
            {
                return await _matriculaRepository.DeleteAsync(alunoId, cursoId);
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<AlunoModel>> GetAlunosByCursoAsync(int cursoId)
        {
            return await _matriculaRepository.GetAlunosByCursoAsync(cursoId);
        }
    }
}
