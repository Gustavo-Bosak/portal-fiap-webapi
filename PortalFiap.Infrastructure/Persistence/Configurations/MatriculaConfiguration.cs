using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalFiap.Domain.Entities;

namespace PortalFiap.Infrastructure.Persistence.Configurations;

public class MatriculaConfiguration : IEntityTypeConfiguration<Matricula>
{
    public void Configure(EntityTypeBuilder<Matricula> builder)
    {
        builder.ToTable("CP_Matriculas");

        builder.HasKey(m => m.Id);
        
        builder.Property(m => m.Active).IsRequired();
        builder.Property(m => m.CreatedAt).IsRequired();
        
        builder.HasOne(m => m.Turma)
            .WithMany(t => t.Matriculas)
            .HasForeignKey("IdTurma")
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(m => m.Bolsa)
            .WithOne()
            .HasForeignKey<Bolsa>(b => b.IdMatricula)
            .OnDelete(DeleteBehavior.Cascade);
    }
}