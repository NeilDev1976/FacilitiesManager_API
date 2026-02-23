using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FacilitiesCoordinator.Domain.Entities;

namespace FacilitiesCoordinator.Infrastructure.Data.Configurations;

public class FacilityGroupConfiguration : IEntityTypeConfiguration<FacilityGroup>
{
    public void Configure(EntityTypeBuilder<FacilityGroup> builder)
    {
        builder.ToTable("FacilityGroups");

        builder.HasKey(g => g.Id);

        builder.Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(g => g.CreatedAt)
            .IsRequired();
    }
}