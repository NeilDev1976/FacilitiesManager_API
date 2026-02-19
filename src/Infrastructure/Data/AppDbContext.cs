namespace FacilitiesCoordinator.Infrastructure.Data;

using FacilitiesCoordinator.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }


    public DbSet<Facility> Facilities => Set<Facility>();
    public DbSet<User> Users => Set<User>();
    public DbSet<FacilityStatusHistory> FacilityStatusHistories => Set<FacilityStatusHistory>();
    public DbSet<FacilityStatusNote> FacilityStatusNotes => Set<FacilityStatusNote>();

}