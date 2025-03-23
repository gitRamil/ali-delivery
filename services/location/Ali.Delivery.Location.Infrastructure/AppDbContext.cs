using Ali.Delivery.Location.Domain.Entities;
using Ali.Delivery.Location.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Location.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<UserLocation> UserLocations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserLocationConfiguration());
        base.OnModelCreating(modelBuilder);
    }
    
}
