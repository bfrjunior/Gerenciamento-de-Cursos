using System.ComponentModel.DataAnnotations;

namespace Gerenciamento_cursos.Dto
{
    public class MatricularDto
    {
        [Required(ErrorMessage = "O ID do aluno é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O ID do aluno deve ser um número válido.")]
        public int AlunoId { get; set; }

        [Required(ErrorMessage = "O ID do curso é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O ID do curso deve ser um número válido.")]
        public int CursoId { get; set; }
    }
}