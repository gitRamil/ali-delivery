using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.RolePermission;

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
    /// <param name="token">Токен.</param>
    /// <param name="roleId">Идентификатор роли пользователя.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если любой из параметров <paramref name="permission" />,
    /// <paramref name="token" />, <paramref name="roleId" /> равен <c>null</c>.
    /// </exception>
    public RolePermission(SequentialGuid id, Permission permission, RolePermissionToken token, SequentialGuid roleId)
        : base(id)
    {
        Permission = permission ?? throw new ArgumentNullException(nameof(permission));
        Token = token ?? throw new ArgumentNullException(nameof(token));
        RoleId = roleId;
    }

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="RolePermission" /> для использования ORM.
    /// </summary>
    /// <remarks>Конструктор без параметров необходим для Entity Framework.</remarks>
    protected RolePermission()
        : base(SequentialGuid.Empty)
    {
        Permission = null!;
        Token = RolePermissionToken.Generate();
        RoleId = SequentialGuid.Empty;
    }

    /// <summary>
    /// Доступ
    /// </summary>
    public virtual Permission Permission { get; private set; }

    /// <summary>
    /// Связанная роль.
    /// </summary>
    public virtual Role Role { get; private set; } = null!;

    /// <summary>
    /// Идентификатор роли.
    /// </summary>
    public SequentialGuid RoleId { get; private set; }

    /// <summary>
    /// Токен.
    /// </summary>
    public RolePermissionToken Token { get; private set; }
}
