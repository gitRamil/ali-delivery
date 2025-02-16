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
    /// Возвращает набор незарегистрированных пользователей.
    /// </summary>
    /// <value>
    /// Незарегистрированные пользователи.
    /// </value>
    DbSet<NotAuthUser> NotAuthUsers { get; }

    /// <summary>
    /// Возвращает набор заказов.
    /// </summary>
    /// <value>
    /// Заказы.
    /// </value>
    DbSet<Domain.Entities.Order> Orders { get; }

    /// <summary>
    /// Возвращает набор доступов.
    /// </summary>
    /// <value>
    /// Доступы.
    /// </value>
    DbSet<Permission> Permissions { get; }

    /// <summary>
    /// Возвращает набор доступов по ролям.
    /// </summary>
    /// <value>
    /// Доступы по ролям.
    /// </value>
    DbSet<RolePermission> RolePermissions { get; }

    /// <summary>
    /// Возвращает набор ролей пользователя. 
    /// </summary>
    /// <value>
    /// Роли.
    /// </value>
    DbSet<Role> Roles { get; }
    
    /// <summary>
    /// Возвращает набор типов паспорта.
    /// </summary>
    /// <value>
    /// Типы паспорта. 
    /// </value>
    DbSet<PassportType> Types { get; }
    
    /// <summary>
    /// Возвращает набор размеров посылки.
    /// </summary>
    /// <value>
    /// Размеры посылки. 
    /// </value>
    DbSet<Size> Sizes { get; }

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
