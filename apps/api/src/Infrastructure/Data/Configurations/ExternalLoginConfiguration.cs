using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FacilitiesCoordinator.Domain.Entities;

namespace FacilitiesCoordinator.Infrastructure.Data.Configurations;

public class ExternalLoginConfiguration : IEntityTypeConfiguration<ExternalLogin>
{
    public void Configure(EntityTypeBuilder<ExternalLogin> builder)
    {
        builder.HasIndex(x => new { x.Provider, x.Issuer, x.Subject })
              .IsUnique();

        builder.Property(x => x.Provider)
              .HasMaxLength(50)
              .IsRequired();

        builder.Property(x => x.Issuer)
              .HasMaxLength(300)
              .IsRequired();

        builder.Property(x => x.Subject)
              .HasMaxLength(200)
              .IsRequired();

        builder.HasOne(x => x.User)
              .WithMany(u => u.ExternalLogins)
              .HasForeignKey(x => x.UserId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}