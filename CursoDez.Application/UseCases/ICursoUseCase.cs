using CursoDez.Application.DTOs;

namespace CursoDez.Application.UseCases
{
    public interface ICursoUseCase
    {
        Task<List<CursoDTO>> GetCursos(int idCurso);
    }
}
