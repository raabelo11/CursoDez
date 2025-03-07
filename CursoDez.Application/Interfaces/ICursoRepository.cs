using CursoDez.Domain.Models;

namespace CursoDez.Application.Interfaces
{
    public interface ICursoRepository
    {
        Task<IEnumerable<Curso>> GetCursoAsync(int idCurso);
    }
}
