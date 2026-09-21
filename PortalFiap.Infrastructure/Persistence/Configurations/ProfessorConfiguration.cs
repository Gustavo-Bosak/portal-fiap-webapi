using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalFiap.Domain.Entities;

namespace PortalFiap.Infrastructure.Persistence.Configurations;

public class ProfessorConfiguration : IEntityTypeConfiguration<Professor>
{
    public void Configure(EntityTypeBuilder<Professor> builder)
    {
        builder.ToTable("CP_Professores");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedNever();

        builder.Property(p => p.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Email)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(p => p.Telefone)
            .HasMaxLength(20)
            .IsRequired();

        // DataNasc é privada em Pessoa: sem este mapeamento o EF não persiste a data (Idade saía errada).
        builder.Property<DateOnly>("DataNasc")
            .HasColumnName("DataNascimento")
            .IsRequired();

        builder.Property(p => p.Active)
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.HasOne(p => p.Endereco)
            .WithMany()
            .HasForeignKey("IdEndereco")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Turmas)
            .WithMany(t => t.Professores)
            .UsingEntity(j => j.ToTable("CP_Turma_Professores"));
    }
}
