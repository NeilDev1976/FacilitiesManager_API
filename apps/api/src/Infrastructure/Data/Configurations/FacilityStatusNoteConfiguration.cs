using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FacilitiesCoordinator.Domain.Entities;

namespace FacilitiesCoordinator.Infrastructure.Data.Configurations;

public class FacilityStatusNoteConfiguration : IEntityTypeConfiguration<FacilityStatusNote>
{
    public void Configure(EntityTypeBuilder<FacilityStatusNote> builder)
    {
        builder.ToTable("FacilityStatusNotes");

        builder.HasKey(n => n.Id);

        builder.Property(n => n.NoteText)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(n => n.CreatedAt)
            .IsRequired();

        builder.HasOne(n => n.CreatedByUser)
            .WithMany()
            .HasForeignKey(n => n.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}