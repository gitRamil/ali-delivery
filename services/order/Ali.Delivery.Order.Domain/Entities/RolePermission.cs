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
    /// <param name="roleId">Доступ.</param>
    /// <param name="permissionId">Идентификатор роли пользователя.</param>
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
            .Concat(GetCourierPermissions());

    private static IEnumerable<RolePermission> GetBasicUserPermissions()
    {
        yield return new RolePermission(new Guid("3a174d9d-c0db-4d94-ec59-c6cf4e208981"), Role.BasicUser.Id, Permission.UserManagement.Id);
        yield return new RolePermission(new Guid("3a174d9d-c0d6-4039-8fc1-bd00fe7d8724"), Role.BasicUser.Id, Permission.OrderManagement.Id);
    }

    private static IEnumerable<RolePermission> GetCourierPermissions()
    {
        yield return new RolePermission(new Guid("3a174d9d-c0d9-1ae6-97f3-4cf384519fe5"), Role.Courier.Id, Permission.OrderManagement.Id);
        yield return new RolePermission(new Guid("3a174d9d-c0da-816f-c8bc-d870eec16d5b"), Role.Courier.Id, Permission.UserManagement.Id);
    }

    private static IEnumerable<RolePermission> GetNotAuthUserPermissions()
    {
        yield return new RolePermission(new Guid("3a174d9d-c0dd-8c7a-dd0b-1aa0c32ce21a"), Role.NotAuthUser.Id, Permission.Tracking.Id);
    }
}
