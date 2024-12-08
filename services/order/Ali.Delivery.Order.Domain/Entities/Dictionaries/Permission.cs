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
    /// Возвращает тип доступа: Доступ создания пользователя.
    /// </summary>
    public static readonly Permission CreateUser = new(new Guid("3a166cc9-799c-27fb-42d7-c1cc8512aeef"),
                                                          new PermissionCode("createUser"),
                                                          new PermissionName("Доступ создания пользователя"));

    /// <summary>
    /// Возвращает тип доступа: Доступ обновления заказа.
    /// </summary>
    public static readonly Permission UpdateOrder = new(new Guid("3a166cc9-7999-9fc9-2798-85b0ef75288d"),
                                                       new PermissionCode("updateOrder"),
                                                       new PermissionName("Доступ обновления заказа"));

    /// <summary>
    /// Возвращает тип доступа: Доступ просмотра заказа.
    /// </summary>
    public static readonly Permission GetOrder = new(new Guid("3a166cc9-799d-6b4b-087a-93e047e60d91"),
                                                              new PermissionCode("getOrder"),
                                                              new PermissionName("Доступ просмотра заказа"));

    /// <summary>
    /// Возвращает тип доступа: Доступ удаления заказа.
    /// </summary>
    public static readonly Permission DeleteOrder = new(new Guid("3a166cc9-799b-7735-e11f-57780f8b0f28"),
                                                       new PermissionCode("deleteOrder"),
                                                       new PermissionName("Доступ удаления заказа"));

    private static readonly Dictionary<PermissionCode, Permission> PermissionNames = new()
    {
        [CreateUser.Code] = CreateUser,
        [UpdateOrder.Code] = UpdateOrder,
        [GetOrder.Code] = GetOrder,
        [DeleteOrder.Code] = DeleteOrder
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