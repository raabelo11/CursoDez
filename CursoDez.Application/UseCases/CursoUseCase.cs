using CursoDez.Application.DTOs;
using CursoDez.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata.Ecma335;

namespace CursoDez.Application.UseCases
{
    public class CursoUseCase : ICursoUseCase
    {
        private readonly ICursoRepository _cursoRepository;
        private readonly ILogger<CursoUseCase> _logger;

        public CursoUseCase(ICursoRepository cursoRepository, ILogger<CursoUseCase> logger)
        {
            _cursoRepository = cursoRepository;
            _logger = logger;
        }

        public async Task<List<CursoDTO>> GetCursos(int idCurso)
        {
            List<CursoDTO> listCursoDTO = new List<CursoDTO>();

            try
            {
                var ret = await _cursoRepository.GetCursoAsync(idCurso);

                listCursoDTO = ret.Select(curso => new CursoDTO
                {
                    CursoId = curso.IdCurso,
                    Nome = curso.NomeCurso,
                    CategoriaCurso = curso.Categoria,
                    CursoAtivo = curso.Ativo
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exceção gerada: {ex.Message}, {ex.InnerException}");
            }

            return listCursoDTO;
        }

        public async Task<bool> CreateCurso(CursoDTO curso)
        {
            bool retorno = false;

            try
            {
                if (curso == null)
                    _logger.LogError("Erro na validação do curso preenchido.");
                else
                {
                    retorno = await _cursoRepository.CreateCursoAsync(curso);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exceção gerada: {ex.Message}, {ex.InnerException}");
            }

            return retorno;
        }
    }
}
