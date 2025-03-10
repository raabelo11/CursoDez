using CursoDez.Application.DTOs;
using CursoDez.Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CursoDez.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AlunoController : Controller
    {
        private readonly IAlunoUseCase _alunoUseCase;
        private readonly ILogger<AlunoController> _logger;

        public AlunoController(IAlunoUseCase alunoUseCase, ILogger<AlunoController> logger)
        {
            _alunoUseCase = alunoUseCase;
            _logger = logger;
        }

        [HttpPost]
        [Route("alunos-matricula")]
        public async Task<IActionResult> PostAluno([FromBody] AlunoDTO alunoDTO)
        {
            var ret = await _alunoUseCase.CreateMatricula(alunoDTO);
            if (!ret)
                return BadRequest("Erro ao criar matrícula do aluno !");

            return Ok("Matrícula criada com sucesso!");
        }
    }
}
