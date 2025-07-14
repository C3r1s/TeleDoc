using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeleDoc.Domain.Entities;

namespace TeleDoc.Data.Configurations;

public class IndividualEntrepreneurConfiguration : IEntityTypeConfiguration<IndividualEntrepreneur>
{
    public void Configure(EntityTypeBuilder<IndividualEntrepreneur> builder)
    {
        builder.Property(ie => ie.FullName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(ie => ie.RegistrationDate)
            .IsRequired(false);
    }
}