using CursoDez.Domain.Enums;

namespace CursoDez.Domain.Models
{
    public class Aluno
    {
        public long IdAluno { get; set; }
        public string NomeAluno { get; set; }
        public string Email { get; set; }
        public int Idade { get; set; }
        public int IdCurso { get; set; }
        public bool MatriculaAtiva { get; set; }
        public StatusPagamento StatusPagamento { get; set; }
        public DateTime DataMatricula { get; set; }
        public DateTime DataVigenciaCurso { get; set; }
    }
}
