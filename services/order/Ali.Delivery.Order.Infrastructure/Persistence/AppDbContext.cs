﻿using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ali.Delivery.Order.Infrastructure.Persistence;

/// <summary>
/// Представляет контекст для работы с БД.
/// </summary>
/// <seealso cref="DbContext" />
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
        : base(options)
    {
        _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
        AttachDictionaryValues();
    }

    /// <summary>
    /// Возвращает набор незарегистрированных пользователей.
    /// </summary>
    public DbSet<NotAuthUser> NotAuthUsers => Set<NotAuthUser>();

    /// <summary>
    /// Возвращает набор заказов.
    /// </summary>
    public DbSet<Domain.Entities.Order> Orders => Set<Domain.Entities.Order>();

    /// <summary>
    /// Возвращает набор статусов заказа.
    /// </summary>
    /// <value>
    /// Статусы заказа.
    /// </value>
    public DbSet<OrderStatus> OrderStatuses => Set<OrderStatus>();

    /// <summary>
    /// Возвращает набор типов паспорта.
    /// </summary>
    public DbSet<PassportType> PassportTypes => Set<PassportType>();

    /// <summary>
    /// Возвращает набор доступов.
    /// </summary>
    public DbSet<Permission> Permissions => Set<Permission>();

    /// <summary>
    /// Возвращает набор доступов по ролям.
    /// </summary>
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();

    /// <summary>
    /// Возвращает набор ролей пользователя.
    /// </summary>
    public DbSet<Role> Roles => Set<Role>();

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

    /// <summary>
    /// Возвращает набор размеров посылки.
    /// </summary>
    public DbSet<Size> Sizes => Set<Size>();

    /// <summary>
    /// Возвращает набор пользователей.
    /// </summary>
    public DbSet<User> Users => Set<User>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    private void AttachDictionaryValues()
    {
        AttachRange(OrderStatus.GetAllValues());
        AttachRange(Role.GetAllValues());
        AttachRange(Size.GetAllValues());
        AttachRange(PassportType.GetAllValues());
        AttachRange(Permission.GetAllValues());
        AttachRange(RolePermission.GetAllValues());
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

    private static void SetEntryProperty(EntityEntry entry, string propertyName, object? value) =>
        entry.Property(propertyName)
             .CurrentValue = value;
}
