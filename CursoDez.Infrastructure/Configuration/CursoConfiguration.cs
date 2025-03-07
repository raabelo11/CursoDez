using CursoDez.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CursoDez.Infrastructure.Configuration
{
    public class CursoConfiguration : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.HasKey(a => a.IdCurso);
            builder.Property(a => a.NomeCurso).HasMaxLength(100).HasColumnType("varchar(100)").IsRequired().HasColumnName("NomeCurso");
            builder.Property(a => a.Categoria).HasConversion<int>().HasColumnType("int").IsRequired().HasColumnName("CategoriaId");
            builder.Property(a => a.Ativo).HasColumnType("bit").IsRequired().HasColumnName("Ativo");
        }
    }
}
