using Ali.Delivery.Order.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Infrastructure.Persistence;

/// <summary>
/// Представляет контекст для работы с БД.
/// </summary>
/// <seealso cref="DbContext" />
public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Возвращает набор заказов.
    /// </summary>
    public DbSet<Domain.Entities.Order> Orders => Set<Domain.Entities.Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}
