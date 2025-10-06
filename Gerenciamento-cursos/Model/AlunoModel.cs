namespace Gerenciamento_cursos.Model
{
    public class AlunoModel
    {

        public int Id { get; set; }


        public string Nome { get; set; }


        public string Email { get; set; }


        public DateTime DataNascimento { get; set; }


        public ICollection<MatriculaModel> Matriculas { get; set; } = new List<MatriculaModel>();


        public int Idade => DateTime.Today.Year - DataNascimento.Year -
            (DateTime.Today.Date < DataNascimento.Date.AddYears(DateTime.Today.Year - DataNascimento.Year) ? 1 : 0);
    }
}
