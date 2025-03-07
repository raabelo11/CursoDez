using CursoDez.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CursoDez.Infrastructure.Configuration
{
    public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.HasKey(a => a.IdAluno);
            builder.Property(a => a.NomeAluno).HasMaxLength(100).IsRequired().HasColumnType("varchar(100)").HasColumnName("NomeAluno");
            builder.Property(a => a.Email).HasMaxLength(100).IsRequired().HasColumnType("varchar(100)").HasColumnName("Email");
            builder.Property(a => a.Idade).IsRequired().HasColumnType("int").HasColumnName("Idade");
            builder.Property(a => a.IdCurso).IsRequired().HasColumnType("int").HasColumnName("IdCurso");
            builder.Property(a => a.MatriculaAtiva).IsRequired().HasColumnType("bit").HasColumnName("MatriculaAtiva");
            builder.Property(a => a.StatusPagamento).HasConversion<int>().HasColumnType("int").IsRequired();
            builder.Property(a => a.DataMatricula).HasColumnType("datetime").IsRequired();
            builder.Property(a => a.DataVigenciaCurso).HasColumnType("datetime").IsRequired();
        }
    }
}
