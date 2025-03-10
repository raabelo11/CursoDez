using CursoDez.Application.DTOs;

namespace CursoDez.Application.Interfaces
{
    public interface IAlunoRepository
    {
        Task<bool> CreateMatriculaAsync(AlunoDTO alunoDTO);
    }
}
