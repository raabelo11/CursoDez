using CursoDez.Application.DTOs;
using CursoDez.Application.Interfaces;
using CursoDez.Domain.Enums;
using CursoDez.Domain.Models;
using CursoDez.Infrastructure.Context;

namespace CursoDez.Infrastructure.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly CursoDezContextSQLServer _context;

        public AlunoRepository(CursoDezContextSQLServer context)
        {
            _context = context;
        }

        public async Task<bool> CreateMatriculaAsync(AlunoDTO alunoDTO)
        {
            bool retorno = false;

            try
            {
                Aluno alunoBase = new()
                {
                    NomeAluno = alunoDTO.NomeAluno,
                    Email = alunoDTO.Email,
                    Idade = alunoDTO.Idade,
                    MatriculaAtiva = false,
                    DataMatricula = DateTime.Now,
                    DataVigenciaCurso = DateTime.Now.AddYears(1),
                    IdCurso = alunoDTO.IdCurso,
                    StatusPagamento = (StatusPagamento) 1
                };

                await _context.Alunos.AddAsync(alunoBase);
                await _context.SaveChangesAsync();

                retorno = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao matricular o aluno na base: {ex.Message}, {ex.InnerException}");
            }

            return retorno;
        }
    }
}
