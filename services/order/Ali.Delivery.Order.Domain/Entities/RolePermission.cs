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
            .Concat(GetCourierPermissions());

    private static IEnumerable<RolePermission> GetBasicUserPermissions()
    {
        yield return new RolePermission(new Guid("3a17be5f-7b9c-0bc4-a94f-b0bba09fd370"), Role.BasicUser.Id, Permission.UserOrderManagement.Id);
        yield return new RolePermission(new Guid("3a17be5f-7bbe-2559-4048-48a0196e0f25"), Role.BasicUser.Id, Permission.Tracking.Id);
        yield return new RolePermission(new Guid("3a17be5f-7bbf-849a-7d45-02bd0f50536d"), Role.BasicUser.Id, Permission.UserManagement.Id);
    }

    private static IEnumerable<RolePermission> GetCourierPermissions()
    {
        yield return new RolePermission(new Guid("3a17be5f-7bc0-509f-e1ec-2f10d0cebef0"), Role.Courier.Id, Permission.CourierOrderManagement.Id);
        yield return new RolePermission(new Guid("3a17be5f-7bc1-6952-64c0-1c9ca8881fd0"), Role.Courier.Id, Permission.UserManagement.Id);
        yield return new RolePermission(new Guid("3a17be5f-7bc2-9c5b-8a73-bcd6ce135188"), Role.Courier.Id, Permission.Tracking.Id);
    }

    private static IEnumerable<RolePermission> GetNotAuthUserPermissions()
    {
        yield return new RolePermission(new Guid("3a17be5f-7bc6-ba8e-0f5d-b7192c99492b"), Role.NotAuthUser.Id, Permission.Tracking.Id);
    }
}
