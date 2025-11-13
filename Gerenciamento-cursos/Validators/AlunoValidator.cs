using Gerenciamento_cursos.Data;
using Gerenciamento_cursos.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Gerenciamento_cursos.Validators
{
    public interface IAlunoValidator
    {
        Task<ValidationResultModel> ValidateAsync(AlunoModel aluno, bool isUpdate = false);
    }

    public class AlunoValidator : IAlunoValidator
    {
        private readonly AppDbContext _context;
        private readonly EmailAddressAttribute _emailValidator = new();

        public AlunoValidator(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ValidationResultModel> ValidateAsync(AlunoModel aluno, bool isUpdate = false)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(aluno.Nome) || aluno.Nome.Length < 3 || aluno.Nome.Length > 100)
                errors.Add("O nome deve ter entre 3 e 100 caracteres.");

            if (string.IsNullOrWhiteSpace(aluno.Email) || aluno.Email.Length > 150)
                errors.Add("O email é obrigatório e não pode exceder 150 caracteres.");

            if (!_emailValidator.IsValid(aluno.Email))
                errors.Add("O email informado não é válido.");

            var emailExists = await _context.Alunos
                .AnyAsync(a => a.Email == aluno.Email && (!isUpdate || a.Id != aluno.Id));
            if (emailExists)
                errors.Add("Este email já está registrado.");

            var age = DateTime.Today.Year - aluno.DataNascimento.Year;
            if (aluno.DataNascimento.Date > DateTime.Today.AddYears(-age))
                age--;

            if (age < 18)
                errors.Add("O aluno deve ter pelo menos 18 anos.");

            return errors.Count > 0 
                ? ValidationResultModel.FailureResult(errors)
                : ValidationResultModel.SuccessResult();
        }
    }
}