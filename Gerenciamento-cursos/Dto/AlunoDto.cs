using System.ComponentModel.DataAnnotations;

namespace Gerenciamento_cursos.Dto
{
    public class AlunoDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        public DateTime DataNascimento { get; set; }
    }
}
