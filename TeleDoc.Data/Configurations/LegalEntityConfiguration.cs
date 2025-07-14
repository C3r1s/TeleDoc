using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeleDoc.Domain.Entities;

namespace TeleDoc.Data.Configurations;

public class LegalEntityConfiguration : IEntityTypeConfiguration<LegalEntity>
{
    public void Configure(EntityTypeBuilder<LegalEntity> builder)
    {
        builder.Property(le => le.LegalAddress)
            .IsRequired()
            .HasMaxLength(500);
    }
}