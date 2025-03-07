using CursoDez.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;

namespace CursoDez.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AlunoController : Controller
    {
        private readonly CursoDezContextSQLServer _context;
        public AlunoController(CursoDezContextSQLServer context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAlunos()
        {
            return Ok();
        }
    }
}
