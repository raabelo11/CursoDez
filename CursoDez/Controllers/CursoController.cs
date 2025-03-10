using CursoDez.Application.DTOs;
using CursoDez.Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CursoDez.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CursoController : Controller
    {
        private readonly ICursoUseCase _cursoUseCase;

        public CursoController(ICursoUseCase cursoUseCase)
        {
            _cursoUseCase = cursoUseCase;
        }

        [HttpGet]
        [Route("cursos/{id}")]
        public async Task<IActionResult> GetCurso([FromRoute] int id = 0)
        {
            var ret = await _cursoUseCase.GetCursos(id);
            if (ret.Count == 0)
                return NotFound($"Nenhum curso registrado ou encontrado com o ID: {id}");

            return Ok(ret);
        }

        [HttpPost]
        [Route("cursos-incluir")]
        public async Task<IActionResult> PostCurso([FromBody] CursoDTO curso)
        {
            var ret = await _cursoUseCase.CreateCurso(curso);
            if (!ret)
                return BadRequest($"Não foi possível criar o curso: {curso.Nome}, verifique os LOGS para mais detalhes!");

            return Ok("Curso criado com sucesso !");
        }
    }
}
