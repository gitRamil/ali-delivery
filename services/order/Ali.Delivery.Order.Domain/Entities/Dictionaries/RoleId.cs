using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.Role;
using Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.RoleId;

namespace Ali.Delivery.Order.Domain.Entities.Dictionaries;

/// <summary>
/// Роли пользователей.
/// </summary>
public class RoleId : Entity<SequentialGuid>
{
    /// <summary>
    /// Возвращает тип пользователя: Курьер.
    /// </summary>
    public static readonly RoleId Courier = new(new Guid("3a1537be-fa32-3962-f94d-62f95e6ffcad"), new RoleCode("courier"), new RoleName("Курьер"));

    /// <summary>
    /// Возвращает тип пользователя: Пользователь.
    /// </summary>
    public static readonly RoleId BasicUser = new(new Guid("3a1537bf-cabc-d70c-f42c-012821b898b1"), new RoleCode("basicUser"), new RoleName("Пользователь"));
    
    /// <summary>
    /// Возвращает тип пользователя: Неавторизованный пользователь.
    /// </summary>
    public static readonly RoleId NotAuthUser = new(new Guid("3a1537c0-11f8-d788-90d9-ced196c63397"), new RoleCode("notAuthUser"), new RoleName("Неавторизованный пользователь"));

    private static readonly Dictionary<RoleCode, RoleId> RoleNames = new()
    {
        [Courier.Code] = Courier,
        [BasicUser.Code] = BasicUser,
        [NotAuthUser.Code] = NotAuthUser
    };
    
    
    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="RoleId" />.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="code">Код.</param>
    /// <param name="name">Наименование.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="code" /> или
    /// <paramref name="name" /> равен <c>null</c>.
    /// </exception>
    /// <remarks>Конструктор для EF.</remarks>
    public RoleId (SequentialGuid id, RoleCode code, RoleName name)
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
    public static IReadOnlyCollection<RoleId> GetAllValues() => RoleNames.Values;
}