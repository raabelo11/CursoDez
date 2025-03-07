using CursoDez.Application.DTOs;
using CursoDez.Application.Interfaces;

namespace CursoDez.Application.UseCases
{
    public class CursoUseCase : ICursoUseCase
    {
        private readonly ICursoRepository _cursoRepository;

        public CursoUseCase(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        public async Task<List<CursoDTO>> GetCursos(int idCurso)
        {
            try
            {
                List<CursoDTO> listCursoDTO = new List<CursoDTO>();

                var ret = await _cursoRepository.GetCursoAsync(idCurso);

                listCursoDTO = ret.Select(curso => new CursoDTO
                {
                    CursoId = curso.IdCurso,
                    Nome = curso.NomeCurso,
                    CategoriaCurso = curso.Categoria,
                    CursoAtivo = curso.Ativo
                }).ToList();

                return listCursoDTO;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
