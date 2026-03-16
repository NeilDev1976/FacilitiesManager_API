using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FacilitiesCoordinator.Domain.Entities;

namespace FacilitiesCoordinator.Infrastructure.Data.Configurations;

public class FacilityConfiguration : IEntityTypeConfiguration<Facility>
{
    public void Configure(EntityTypeBuilder<Facility> builder)
    {
        builder.ToTable("Facilities");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Code)
            .IsRequired()
            .HasMaxLength(4);

        builder.HasIndex(f => f.Code)
            .IsUnique();

        builder.Property(f => f.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(f => f.CurrentStatus)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(f => f.CreatedAt)
            .IsRequired();

        builder.Property(f => f.UpdatedAt)
            .IsRequired();

        builder.HasOne(f => f.FacilityGroup)
            .WithMany(g => g.OwnedFacilityList)
            .HasForeignKey(f => f.GroupId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(f => f.StatusHistory)
            .WithOne(h => h.Facility)
            .HasForeignKey(h => h.FacilityId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}