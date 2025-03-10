using CursoDez.Domain.Enums;

namespace CursoDez.Application.DTOs
{
    public class AlunoDTO
    {
        public long IdAluno { get; set; }
        public string NomeAluno { get; set; }
        public string Email { get; set; }
        public int Idade { get; set; }
        public int IdCurso { get; set; }
    }
}
