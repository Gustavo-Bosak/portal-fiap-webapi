using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalFiap.Domain.Entities;

namespace PortalFiap.Infrastructure.Persistence.Configurations;

public class TurmaConfiguration : IEntityTypeConfiguration<Turma>
{
    public void Configure(EntityTypeBuilder<Turma> builder)
    {
        builder.ToTable("CP_Turmas");

        builder.HasKey(t => t.Id);
        
        builder.Property(t => t.NomeTurma)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(t => t.AnoLetivo)
            .IsRequired();
        
        builder.Property(t => t.Semestre)
            .IsRequired();
        
        builder.Property(t => t.Active).IsRequired();
        builder.Property(t => t.CreatedAt).IsRequired();

        // One-to-many com Matricula (configurado em MatriculaConfiguration)
        // Many-to-many com Professor (configurado em ProfessorConfiguration)
        // FK com Curso (configurado em CursoConfiguration)
    }
}