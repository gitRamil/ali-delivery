using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.Abstractions;

/// <summary>
/// Описывает контекст взаимодействия с БД.
/// </summary>
public interface IAppDbContext
{
    /// <summary>
    /// Возвращает набор заказов.
    /// </summary>
    /// <value>
    /// Заказы.
    /// </value>
    DbSet<Domain.Entities.Order> Orders { get; }

    DbSet<Domain.Entities.Dictionaries.Permission> Permissions { get; }
    DbSet<RolePermission> RolePermissions { get; }
    DbSet<Role> Roles { get; }

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
