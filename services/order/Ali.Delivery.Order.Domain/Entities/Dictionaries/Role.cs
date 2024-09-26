using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.Role;

namespace Ali.Delivery.Order.Domain.Entities.Dictionaries;

/// <summary>
/// Роли пользователей.
/// </summary>
public class Role : Entity<SequentialGuid>
{
    /// <summary>
    /// Возвращает тип пользователя: Курьер.
    /// </summary>
    public static readonly Role Courier = new(new Guid("3a1537be-fa32-3962-f94d-62f95e6ffcad"), new RoleCode("courier"), new RoleName("Курьер"));

    /// <summary>
    /// Возвращает тип пользователя: Пользователь.
    /// </summary>
    public static readonly Role BasicUser = new(new Guid("3a1537bf-cabc-d70c-f42c-012821b898b1"), new RoleCode("basicUser"), new RoleName("Пользователь"));
    
    /// <summary>
    /// Возвращает тип пользователя: Неавторизованный пользователь.
    /// </summary>
    public static readonly Role NotAuthUser = new(new Guid("3a1537c0-11f8-d788-90d9-ced196c63397"), new RoleCode("notAuthUser"), new RoleName("Неавторизованный пользователь"));

    private static readonly Dictionary<RoleCode, Role> RoleNames = new()
    {
        [Courier.Code] = Courier,
        [BasicUser.Code] = BasicUser,
        [NotAuthUser.Code] = NotAuthUser
    };
    
    
    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="Role" />.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="code">Код.</param>
    /// <param name="name">Наименование.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="code" /> или
    /// <paramref name="name" /> равен <c>null</c>.
    /// </exception>
    /// <remarks>Конструктор для EF.</remarks>
    protected Role (SequentialGuid id, RoleCode code, RoleName name)
        : base(id)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    /// <summary>
    /// Возвращает код.
    /// </summary>
    public RoleCode Code { get; }

    /// <summary>
    /// Возвращает наименование.
    /// </summary>
    public RoleName Name { get; }

    /// <summary>
    /// Возвращает все значения перечисления.
    /// </summary>
    public static IReadOnlyCollection<Role> GetAllValues() => RoleNames.Values;
}
