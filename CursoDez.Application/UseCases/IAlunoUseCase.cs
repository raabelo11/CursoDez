using CursoDez.Application.DTOs;

namespace CursoDez.Application.UseCases
{
    public interface IAlunoUseCase
    {
        Task<bool> CreateMatricula(AlunoDTO alunoDTO);
    }
}
