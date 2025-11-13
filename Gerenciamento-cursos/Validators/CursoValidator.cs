using Gerenciamento_cursos.Model;

namespace Gerenciamento_cursos.Validators
{
    public interface ICursoValidator
    {
        ValidationResultModel Validate(CursoModel curso);
    }

    public class CursoValidator : ICursoValidator
    {
        public ValidationResultModel Validate(CursoModel curso)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(curso.Nome) || curso.Nome.Length < 3 || curso.Nome.Length > 100)
                errors.Add("O nome do curso deve ter entre 3 e 100 caracteres.");

            if (string.IsNullOrWhiteSpace(curso.Descricao) || curso.Descricao.Length < 10 || curso.Descricao.Length > 500)
                errors.Add("A descrição deve ter entre 10 e 500 caracteres.");

            return errors.Count > 0 
                ? ValidationResultModel.FailureResult(errors)
                : ValidationResultModel.SuccessResult();
        }
    }
}