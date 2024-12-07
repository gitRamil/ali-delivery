using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;

namespace Ali.Delivery.Order.Domain.Entities;

/// <summary>
/// Представляет сущность доступа по роли.
/// </summary>
public class RolePermission : Entity<SequentialGuid>
{
    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="RolePermission" />.
    /// </summary>
    /// <param name="id">Идентификатор доступа.</param>
    /// <param name="permission">Доступ.</param>
    /// <param name="role">Идентификатор роли пользователя.</param>
    /// <param name="token">JWT токен.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если любой из параметров <paramref name="permission" />, <paramref name="role" /> или <paramref name="token" /> равен <c>null</c>.
    /// </exception>
    public RolePermission(SequentialGuid id, Permission permission, Role role, string token)
        : base(id)
    {
        Permission = permission ?? throw new ArgumentNullException(nameof(permission));
        Role = role ?? throw new ArgumentNullException(nameof(role));
        Token = token ?? throw new ArgumentNullException(nameof(token));
    }

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="RolePermission" /> для использования ORM.
    /// </summary>
    /// <remarks>Конструктор без параметров необходим для Entity Framework.</remarks>
    protected RolePermission()
        : base(SequentialGuid.Empty)
    {
        Permission = null!;
        Role = null!;
        Token = string.Empty;
    }

    /// <summary>
    /// Доступ.
    /// </summary>
    public virtual Permission Permission { get; private set; }

    /// <summary>
    /// Связанная роль.
    /// </summary>
    public virtual Role Role { get; private set; }

    // /// <summary>
    // /// Идентификатор роли.
    // /// </summary>
    // public Guid RoleId { get; private set; }
    
    /// <summary>
    /// JWT токен.
    /// </summary>
    public string Token { get; private set; }
}
