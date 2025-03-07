using Microsoft.EntityFrameworkCore;
using CursoDez.Domain.Models;

namespace CursoDez.Infrastructure.Context;

public class CursoDezContextSQLServer : DbContext
{
    public CursoDezContextSQLServer(DbContextOptions<CursoDezContextSQLServer> options) : base(options) 
    {

    }

    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Curso> Cursos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CursoDezContextSQLServer).Assembly);
    }
}
