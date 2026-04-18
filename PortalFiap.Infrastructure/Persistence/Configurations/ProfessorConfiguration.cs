using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalFiap.Domain.Entities;

namespace PortalFiap.Infrastructure.Persistence.Configurations;

public class ProfessorConfiguration : IEntityTypeConfiguration<Professor>
{
    public void Configure(EntityTypeBuilder<Professor> builder)
    {
        builder.ToTable("CP_Professores");
        
        builder.HasMany(p => p.Turmas)
            .WithMany(t => t.Professores)
            .UsingEntity(j => j.ToTable("CP_Turma_Professores"));
    }
}