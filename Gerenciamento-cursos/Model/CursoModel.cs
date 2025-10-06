namespace Gerenciamento_cursos.Model
{
    public class CursoModel
    {

        public int Id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public ICollection<MatriculaModel> Matriculas { get; set; } = new List<MatriculaModel>();
    }
}
