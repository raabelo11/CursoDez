﻿using CursoDez.Application.Interfaces;
using CursoDez.Domain.Models;
using CursoDez.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CursoDez.Infrastructure.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly CursoDezContextSQLServer _context;

        public CursoRepository(CursoDezContextSQLServer context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Curso>> GetCursoAsync(int idCurso)
        {
            try
            {
                var ret =  await _context.Cursos.AsNoTracking().ToListAsync();

                if (ret == null)
                    throw new Exception("Nenhum curso foi encontrado");

                else
                {
                    if (idCurso != 0)
                    {
                        var findCursobyId = ret.FirstOrDefault(p => p.IdCurso == idCurso) ?? throw new Exception($"Nenhum curso com o id: {idCurso}, foi encontrado");

                        return new List<Curso> { findCursobyId };
                    }

                    return ret;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro encontrado: {ex.Message}");
            }
        }
    }
}
