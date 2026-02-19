using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FacilitiesCoordinator.Domain.Entities;

namespace FacilitiesCoordinator.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Username)
            .IsRequired()
            .HasMaxLength(30);

        builder.HasIndex(f => f.Username)
            .IsUnique();

        builder.Property(f => f.FirstName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(f => f.LastName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(f => f.Role)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(f => f.IsActive)
            .IsRequired();

        builder.Property(f => f.CreatedAt)
            .IsRequired();
    }
}