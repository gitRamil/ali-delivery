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
    /// <param name="id">Идентификатор.</param>
    /// <param name="roleId">Идентификатор роли.</param>
    /// <param name="permissionId">Идентификатор доступа.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если любой из параметров <paramref name="roleId" />,
    /// <paramref name="permissionId" /> равен <c>null</c>.
    /// </exception>
    public RolePermission(SequentialGuid id, SequentialGuid roleId, SequentialGuid permissionId)
        : base(id)
    {
        RoleId = roleId;
        PermissionId = permissionId;
    }

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="RolePermission" /> для использования ORM.
    /// </summary>
    /// <remarks>Конструктор без параметров необходим для Entity Framework.</remarks>
    protected RolePermission()
        : base(SequentialGuid.Empty)
    {
        RoleId = SequentialGuid.Empty;
        PermissionId = SequentialGuid.Empty;
    }

    /// <summary>
    /// Возвращает или устанавливает разрешение.
    /// </summary>
    public virtual Permission? Permission { get; private set; }

    /// <summary>
    /// Возвращает или устанавливает идентификатор разрешения.
    /// </summary>
    public SequentialGuid PermissionId { get; set; }

    /// <summary>
    /// Возвращает или устанавливает роль.
    /// </summary>
    public virtual Role? Role { get; private set; }

    /// <summary>
    /// Возвращает или устанавливает идентификатор роли.
    /// </summary>
    public SequentialGuid RoleId { get; set; }

    /// <summary>
    /// Возвращает все значения доступов по роли.
    /// </summary>
    public static IEnumerable<RolePermission> GetAllValues() =>
        GetNotAuthUserPermissions()
            .Concat(GetBasicUserPermissions())
            .Concat(GetCourierPermissions())
            .Concat(GetAdminPermissions());

    private static IEnumerable<RolePermission> GetBasicUserPermissions()
    {
        yield return new RolePermission(new Guid("3a1844e7-409f-dd6f-39ed-1f2f67aef722"), Role.BasicUser.Id, Permission.UserOrderManagement.Id);
        yield return new RolePermission(new Guid("3a1844e7-40b0-0bdb-5ad3-309f135ff5e2"), Role.BasicUser.Id, Permission.Tracking.Id);
        yield return new RolePermission(new Guid("3a1844e7-40b1-d958-4573-cc84183ec531"), Role.BasicUser.Id, Permission.UserManagement.Id);
    }

    private static IEnumerable<RolePermission> GetCourierPermissions()
    {
        yield return new RolePermission(new Guid("3a1844e7-40b2-9ae9-56c9-27598b808a43"), Role.Courier.Id, Permission.CourierOrderManagement.Id);
        yield return new RolePermission(new Guid("3a1844e7-40b3-790d-6f17-ffa82729df95"), Role.Courier.Id, Permission.UserManagement.Id);
        yield return new RolePermission(new Guid("3a1844e7-40b4-6c99-9ee8-c6ed4958ad43"), Role.Courier.Id, Permission.Tracking.Id);
    }

    private static IEnumerable<RolePermission> GetNotAuthUserPermissions()
    {
        yield return new RolePermission(new Guid("3a1844e7-40b5-634c-f5e0-662dfe13dff1"), Role.NotAuthUser.Id, Permission.Tracking.Id);
    }

    private static IEnumerable<RolePermission> GetAdminPermissions()
    {
        yield return new RolePermission(new Guid("3a1844e7-40b6-8c81-44cf-f1ff77037740"), Role.SuperUser.Id, Permission.FullAccess.Id);
    }
}
