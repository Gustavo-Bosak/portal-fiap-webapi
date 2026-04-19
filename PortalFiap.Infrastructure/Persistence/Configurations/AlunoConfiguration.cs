using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalFiap.Domain.Entities;

namespace PortalFiap.Infrastructure.Persistence.Configurations;

public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
{
    public void Configure(EntityTypeBuilder<Aluno> builder)
    {
        builder.ToTable("CP_Alunos");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.Email)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(a => a.Telefone)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(a => a.Active)
            .IsRequired();

        builder.Property(a => a.CreatedAt)
            .IsRequired();

        builder.HasOne(a => a.Endereco)
            .WithMany()
            .HasForeignKey("IdEndereco")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(a => a.Matriculas)
            .WithOne(m => m.Aluno)
            .HasForeignKey("IdAluno")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
