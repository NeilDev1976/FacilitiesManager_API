using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FacilitiesCoordinator.Domain.Entities;

namespace FacilitiesCoordinator.Infrastructure.Data.Configurations;

public class StaffConfiguration : IEntityTypeConfiguration<Staff>
{
    public void Configure(EntityTypeBuilder<Staff> builder)
    {
        builder.ToTable("Staff");


        builder.HasKey(u => u.StaffId);

        builder.HasOne(s => s.Facility)
            .WithMany(f => f.Staff)
            .HasForeignKey(s => s.FacilityId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.NameNormalized)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.MinimumStaffRequired)
            .IsRequired();

        builder.Property(u => u.CurrentStaffCount)
            .IsRequired();

        builder.Property(u => u.UpdatedAt)
            .IsRequired();

        builder.HasIndex(s => new { s.FacilityId, s.NameNormalized })
            .IsUnique();
    }
}