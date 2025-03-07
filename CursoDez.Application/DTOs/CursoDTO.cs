using CursoDez.Domain.Enums;

namespace CursoDez.Application.DTOs
{
    public class CursoDTO
    {
        public long CursoId { get; set; }
        public string Nome { get; set; }
        public CategoriaCurso CategoriaCurso { get; set; }
        public bool CursoAtivo { get; set; }
    }
}
