using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalFiap.Domain.Commom;

namespace PortalFiap.Infrastructure.Persistence.Configurations;

public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
{
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        builder.ToTable("CP_Pessoas");

        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Nome)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(p => p.Email)
            .HasMaxLength(150)
            .IsRequired();
        
        builder.Property(p => p.Telefone)
            .HasMaxLength(20)
            .IsRequired();
        
        builder.Property(p => p.Active)
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .IsRequired();
        
        builder.HasOne(p => p.Endereco)
            .WithMany()
            .HasForeignKey("IdEndereco")
            .OnDelete(DeleteBehavior.Restrict);
    }
}