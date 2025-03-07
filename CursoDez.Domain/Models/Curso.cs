using CursoDez.Domain.Enums;

namespace CursoDez.Domain.Models
{
    public class Curso
    {
        public long IdCurso { get; set; }
        public string NomeCurso { get; set; }
        public CategoriaCurso Categoria { get; set; }
        public bool Ativo { get; set; }
    }
}
