using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalFiap.Domain.Entities;

namespace PortalFiap.Infrastructure.Persistence.Configurations;

public class CursoConfiguration : IEntityTypeConfiguration<Curso>
{
    public void Configure(EntityTypeBuilder<Curso> builder)
    {
        builder.ToTable("CP_Cursos");

        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Nome)
            .HasConversion<String>()
            .IsRequired();
        
        builder.Ignore(c => c.Sigla);
        
        builder.Property(c => c.CargaHoraria)
            .IsRequired();
        
        builder.Property(c => c.Active).IsRequired();
        builder.Property(c => c.CreatedAt).IsRequired();
        
        builder.HasMany(c => c.Turmas)
            .WithOne(t => t.Curso)
            .HasForeignKey("IdCurso")
            .OnDelete(DeleteBehavior.Restrict);
    }
}