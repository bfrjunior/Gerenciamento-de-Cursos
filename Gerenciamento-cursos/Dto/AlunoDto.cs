using System.ComponentModel.DataAnnotations;

namespace Gerenciamento_cursos.Dto
{
    public class AlunoDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, MinimumLength = 3,
             ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email informado não é válido.")]
        [StringLength(150, ErrorMessage = "O email não pode exceder 150 caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "A data de nascimento deve ser válida.")]
        [BirthDateValidation(ErrorMessage = "O aluno deve ter pelo menos 18 anos.")]
        public DateTime DataNascimento { get; set; }
    }

    // Validação customizada para data de nascimento
    public class BirthDateValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is not DateTime date)
                return false;

            var age = DateTime.Today.Year - date.Year;
            if (date.Date > DateTime.Today.AddYears(-age))
                age--;

            return age >= 18;
        }
    }
}
