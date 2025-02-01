using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.Permission;

namespace Ali.Delivery.Order.Domain.Entities.Dictionaries;

/// <summary>
/// Представляет доступы для пользователей.
/// </summary>
public class Permission : Entity<SequentialGuid>
{
    /// <summary>
    /// Возвращает тип доступа: Полный доступ.
    /// </summary>
    public static readonly Permission FullAccess = new(new Guid("3a17be54-4e65-5dca-866e-f7cd3b8c49bb"), new PermissionCode(1003), new PermissionName("Полный доступ"));

    /// <summary>
    /// Возвращает тип доступа: Доступ работы с заказами.
    /// </summary>
    public static readonly Permission OrderManagement =
        new(new Guid("3a17be54-4e80-5c77-723b-dc0991c878da"), new PermissionCode(1001), new PermissionName("Доступ для работы с заказами"));

    /// <summary>
    /// Возвращает тип доступа: Доступ для отслеживания.
    /// </summary>
    public static readonly Permission Tracking = new(new Guid("3a17be54-4e81-b978-5d49-940a8c2da6ab"), new PermissionCode(1002), new PermissionName("Доступ отслеживания заказов"));

    /// <summary>
    /// Возвращает тип доступа: Доступ пользователя к работе с заказами.
    /// </summary>
    public static readonly Permission UserOrderManagement = new(new Guid("3a17be54-4e82-4bfb-f422-2caeb3389561"),
                                                           new PermissionCode(1004),
                                                           new PermissionName("Доступ пользователя к работе с заказами"));
    /// <summary>
    /// Возвращает тип доступа: Доступ курьера к работе с заказами.
    /// </summary>
    public static readonly Permission CourierOrderManagement = new(new Guid("3a17be54-4e83-e173-197a-4a19535ed222"),
                                                           new PermissionCode(1005),
                                                           new PermissionName("Доступ курьера к работе с заказами"));
    /// <summary>
    /// Возвращает тип доступа: Доступ для работы с сущностью пользователя.
    /// </summary>
    public static readonly Permission UserManagement = new(new Guid("3a17be54-4e84-3e77-9188-a292bac6366c"),
                                                           new PermissionCode(1000),
                                                           new PermissionName("Доступ для работы с сущностью пользователя"));
    

    private static readonly Dictionary<PermissionCode, Permission> PermissionNames = new()
    {
        [UserManagement.Code] = UserManagement,
        [FullAccess.Code] = FullAccess,
        [Tracking.Code] = Tracking,
        [OrderManagement.Code] = OrderManagement,
        [UserOrderManagement.Code] = UserOrderManagement,
        [CourierOrderManagement.Code] = CourierOrderManagement
    };

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="Permission" />.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="code">Код.</param>
    /// <param name="name">Наименование.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="code" /> или
    /// <paramref name="name" /> равен <c>null</c>.
    /// </exception>
    /// <remarks>Конструктор для EF.</remarks>
    public Permission(SequentialGuid id, PermissionCode code, PermissionName name)
        : base(id)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    /// <summary>
    /// Возвращает код.
    /// </summary>
    public PermissionCode Code { get; }

    /// <summary>
    /// Возвращает наименование.
    /// </summary>
    public PermissionName Name { get; }

    /// <summary>
    /// Возвращает все значения перечисления.
    /// </summary>
    public static IReadOnlyCollection<Permission> GetAllValues() => PermissionNames.Values;
}
