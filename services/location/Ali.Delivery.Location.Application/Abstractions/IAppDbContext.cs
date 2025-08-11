using Ali.Delivery.Location.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Location.Application.Abstractions;

/// <summary>
/// Описывает контекст взаимодействия с БД.
/// </summary>
public interface IAppDbContext
{
    /// <summary>
    /// Возвращает набор конфигураций пользователей.
    /// </summary>
    /// <value>
    /// Конфигурации пользователей.
    /// </value>
    DbSet<UserConfig> UserConfigs { get; }

    /// <summary>
    /// Возвращает набор локаций пользователей.
    /// </summary>
    /// <value>
    /// Локации пользователи.
    /// </value>
    DbSet<UserLocation> UserLocations { get; }

    /// <summary>
    /// Возвращает набор пользователей.
    /// </summary>
    /// <value>
    /// Пользователи.
    /// </value>
    DbSet<User> Users { get; }

    /// <summary>
    /// Сохраняет изменения.
    /// </summary>
    /// <param name="cancellationToken">Маркер отмены.</param>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
