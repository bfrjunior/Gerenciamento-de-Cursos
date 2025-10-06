using System.ComponentModel.DataAnnotations;

namespace Gerenciamento_cursos.Dto
{
    public class AlunoDto
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }
    }
}
