using System.ComponentModel.DataAnnotations;

namespace Gerenciamento_cursos.Dto
{
    public class CursoDto
    {
        [Required(ErrorMessage = "O nome do curso é obrigatório.")]
        [StringLength(100, MinimumLength = 3,
           ErrorMessage = "O nome do curso deve ter entre 3 e 100 caracteres.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "A descrição do curso é obrigatória.")]
        [StringLength(500, MinimumLength = 10,
            ErrorMessage = "A descrição deve ter entre 10 e 500 caracteres.")]
        public string? Descricao { get; set; }
    }
}
