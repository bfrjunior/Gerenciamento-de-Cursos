using System.ComponentModel.DataAnnotations;

namespace Gerenciamento_cursos.Dto
{
    public class CursoDto
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }
    }
}
