using CursoDez.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace CursoDez.API.Controllers
{
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
            return Ok(ret);
        }
    }
}
