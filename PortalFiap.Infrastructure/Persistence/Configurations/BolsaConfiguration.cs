using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalFiap.Domain.Entities;

namespace PortalFiap.Infrastructure.Persistence.Configurations;

public class BolsaConfiguration : IEntityTypeConfiguration<Bolsa>
{
    public void Configure(EntityTypeBuilder<Bolsa> builder)
    {
        builder.ToTable("CP_Bolsas");

        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.IdMatricula)
            .IsRequired();
        
        builder.Property(b => b.Desconto)
            .HasPrecision(5, 4)
            .IsRequired();
        
        builder.Property(b => b.Validade)
            .IsRequired();
        
        builder.Property(b => b.Active).IsRequired();
        builder.Property(b => b.CreatedAt).IsRequired();
    }
}