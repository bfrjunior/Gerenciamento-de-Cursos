namespace Gerenciamento_cursos.Model
{
    public class MatriculaModel
    {
        public int AlunoId { get; set; }

        public AlunoModel? Aluno { get; set; }

        public int CursoId { get; set; }

        public CursoModel? Curso { get; set; }

        public DateTime DataMatricula { get; set; } = DateTime.Now;
    }
}
