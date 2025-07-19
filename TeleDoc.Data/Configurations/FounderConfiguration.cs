using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeleDoc.Domain.Entities;

namespace TeleDoc.Data.Configurations;

public class FounderConfiguration : IEntityTypeConfiguration<Founder>
{
    public void Configure(EntityTypeBuilder<Founder> builder)
    {
        builder.ToTable("Founders");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.TaxId)
            .IsRequired()
            .HasMaxLength(12);

        builder.Property(f => f.FullName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(f => f.CreatedAt)
            .IsRequired();

        builder.Property(f => f.UpdatedAt)
            .IsRequired();

        builder.HasIndex(f => f.TaxId)
            .IsUnique();

        builder.HasMany(f => f.LegalEntities)
            .WithMany(le => le.Founders)
            .UsingEntity<Dictionary<string, object>>(
                "LegalEntityFounder", // Название связующей таблицы (автоматическое)
                j => j.HasOne<LegalEntity>().WithMany().HasForeignKey("LegalEntityId"),
                j => j.HasOne<Founder>().WithMany().HasForeignKey("FounderId"),
                j => j.ToTable("LegalEntityFounders") // Можно задать имя таблицы
            );
    }
}