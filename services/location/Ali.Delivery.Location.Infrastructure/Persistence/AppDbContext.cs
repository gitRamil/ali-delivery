using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Domain.Entities;
using Ali.Delivery.Location.Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ali.Delivery.Location.Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    private readonly IDateTimeService _dateTimeService;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="AppDbContext" /> с заданными параметрами.
    /// </summary>
    /// <param name="options">Параметры конфигурации для контекста базы данных.</param>
    /// <param name="dateTimeService">Сервис для работы с датой и временем.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="dateTimeService" /> равен <c>null</c>.
    /// </exception>
    public AppDbContext(DbContextOptions<AppDbContext> options, IDateTimeService dateTimeService)
        : base(options) =>
        _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));

    /// <inheritdoc cref="DbContext" />
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<Entity<SequentialGuid>>())
        {
            if (entry.State == EntityState.Added)
            {
                MarkCreated(entry);
            }
            else if (entry.State == EntityState.Modified)
            {
                MarkUpdated(entry);
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<UserLocation> UserLocations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    private void MarkCreated(EntityEntry entry)
    {
        var now = _dateTimeService.GetCurrentDateTime();
        //var userId = _currentUser.IsAuthenticated ? _currentUser.Id.ToString() : SystemUser.Id.ToString();

        SetEntryProperty(entry, EntityBasePropertyNames.CreatedDate, now);
        SetEntryProperty(entry, EntityBasePropertyNames.UpdatedDate, now);
        //SetEntryProperty(entry, EntityBasePropertyNames.CreatedBy, userId);
        //SetEntryProperty(entry, EntityBasePropertyNames.UpdatedBy, userId);
    }

    private void MarkUpdated(EntityEntry entry)
    {
        var now = _dateTimeService.GetCurrentDateTime();
        //var userId = _currentUser.IsAuthenticated ? _currentUser.Id.ToString() : SystemUser.Id.ToString();

        SetEntryProperty(entry, EntityBasePropertyNames.UpdatedDate, now);
        //SetEntryProperty(entry, EntityBasePropertyNames.UpdatedBy, userId);
    }

    private static void SetEntryProperty(EntityEntry entry, string propertyName, object? value)
    {
        entry.Property(propertyName)
             .CurrentValue = value;
    }
}
