using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalFiap.Domain.Entities;

namespace PortalFiap.Infrastructure.Persistence.Configurations;

public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
{
    public void Configure(EntityTypeBuilder<Endereco> builder)
    {
        builder.ToTable("CP_Enderecos");

        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Logradouro)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(e => e.Estado)
            .HasMaxLength(2)
            .IsRequired();
        
        builder.Property(e => e.Cidade)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(e => e.Bairro)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(e => e.Cep)
            .HasMaxLength(8)
            .IsRequired();
        
        builder.Property(e => e.Active).IsRequired();
        builder.Property(e => e.CreatedAt).IsRequired();
    }
}