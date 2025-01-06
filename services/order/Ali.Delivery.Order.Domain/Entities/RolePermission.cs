using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;

namespace Ali.Delivery.Order.Domain.Entities;

/// <summary>
/// Представляет сущность доступа по роли.
/// </summary>
public class RolePermission : Entity<SequentialGuid>
{
    public static readonly RolePermission BasicUserOrderAccess = new(new Guid("3a174d9d-c0d6-4039-8fc1-bd00fe7d8724"), Permission.OrderManagement,Role.BasicUser);
    public static readonly RolePermission CourierOrderAccess = new(new Guid("3a174d9d-c0d9-1ae6-97f3-4cf384519fe5"),Permission.OrderManagement,Role.Courier );
    public static readonly RolePermission CourierUserAccess = new(new Guid("3a174d9d-c0da-816f-c8bc-d870eec16d5b"),Permission.UserManagement,Role.Courier );
    public static readonly RolePermission BasicUserUserAccess = new(new Guid("3a174d9d-c0db-4d94-ec59-c6cf4e208981"),Permission.UserManagement,Role.BasicUser);
    public static readonly RolePermission NotAuthUserAccess = new(new Guid("3a174d9d-c0dd-8c7a-dd0b-1aa0c32ce21a"),Permission.Tracking, Role.NotAuthUser);
    
    
    
    
    private static readonly Dictionary<Guid, RolePermission> PermissionsById = new()
    {
        [BasicUserOrderAccess.Id] = BasicUserOrderAccess,
        [CourierOrderAccess.Id] = CourierOrderAccess,
        [CourierUserAccess.Id] = CourierUserAccess,
        [BasicUserUserAccess.Id] = BasicUserUserAccess,
        [NotAuthUserAccess.Id] = NotAuthUserAccess
    };
    
    
    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="RolePermission" />.
    /// </summary>
    /// <param name="id">Идентификатор доступа.</param>
    /// <param name="permission">Доступ.</param>
    /// <param name="role">Идентификатор роли пользователя.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если любой из параметров <paramref name="permission" />, <paramref name="role" /> равен <c>null</c>.
    /// </exception>
    public RolePermission(SequentialGuid id, Permission permission, Role role)
        : base(id)
    {
        Permission = permission ?? throw new ArgumentNullException(nameof(permission));
        Role = role ?? throw new ArgumentNullException(nameof(role));
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
    }

    /// <summary>
    /// Доступ.
    /// </summary>
    public virtual Permission Permission { get; private set; }

    /// <summary>
    /// Связанная роль.
    /// </summary>
    public virtual Role Role { get; private set; }
    
}
