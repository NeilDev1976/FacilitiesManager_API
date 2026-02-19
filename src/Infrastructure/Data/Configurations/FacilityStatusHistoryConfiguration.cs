using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FacilitiesCoordinator.Domain.Entities;

namespace FacilitiesCoordinator.Infrastructure.Data.Configurations;

public class FacilityStatusHistoryConfiguration 
    : IEntityTypeConfiguration<FacilityStatusHistory>
{
    public void Configure(EntityTypeBuilder<FacilityStatusHistory> builder)
    {
        builder.ToTable("FacilityStatusHistory");

        builder.HasKey(h => h.Id);

        builder.Property(h => h.PreviousStatus)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(h => h.NewStatus)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(h => h.ChangedAt)
            .IsRequired();

        builder.HasOne(h => h.ChangedByUser)
            .WithMany()
            .HasForeignKey(h => h.ChangedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
