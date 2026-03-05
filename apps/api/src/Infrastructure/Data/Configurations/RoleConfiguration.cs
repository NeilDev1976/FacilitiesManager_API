using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FacilitiesCoordinator.Domain.Entities;

namespace FacilitiesCoordinator.Infrastructure.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(r => r.NameNormalized)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(r => r.NameNormalized)
            .IsUnique();

        builder.HasData(
            new Role { Id = 1, Name = "User", NameNormalized = "USER" },
            new Role { Id = 2, Name = "Manager", NameNormalized = "MANAGER" },
            new Role { Id = 3, Name = "Admin", NameNormalized = "ADMIN" }
        );
        }
}