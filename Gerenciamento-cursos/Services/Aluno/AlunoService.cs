using Gerenciamento_cursos.Data;
using Gerenciamento_cursos.Model;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento_cursos.Services.Aluno
{
    public class AlunoService : IAlunoService
    {
        private readonly AppDbContext _context;

        public AlunoService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<AlunoModel>> GetAllAsync() => await _context.Alunos.ToListAsync();
        public async Task<AlunoModel> GetByIdAsync(int id) => await _context.Alunos.FindAsync(id);
        public async Task<bool> DeleteAsync(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null) return false;

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<(bool Success, string ErrorMessage)> AddAsync(AlunoModel aluno)
        {

            if (aluno.Idade < 18)
            {
                return (false, "O aluno deve ter 18 anos ou mais para ser matriculado.");
            }

            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();
            return (true, null);
        }


        public async Task<(bool Success, string ErrorMessage)> UpdateAsync(AlunoModel aluno)
        {

            if (aluno.Idade < 18)
            {
                return (false, "A data de nascimento atualizada indica que o aluno é menor de idade. A matrícula requer 18 anos ou mais.");
            }


            var existingAluno = await _context.Alunos.FindAsync(aluno.Id);
            if (existingAluno == null)
            {
                return (false, "Aluno não encontrado.");
            }


            existingAluno.Nome = aluno.Nome;
            existingAluno.Email = aluno.Email;
            existingAluno.DataNascimento = aluno.DataNascimento;

            try
            {
                await _context.SaveChangesAsync();
                return (true, null);
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!_context.Alunos.Any(e => e.Id == aluno.Id))
                {
                    return (false, "Aluno não encontrado ou conflito de concorrência.");
                }
                throw;
            }
        }
    }
}

