using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.Roles;

namespace Ali.Delivery.Order.Domain.Entities.Dictionaries;

public class Roles : Entity<SequentialGuid>
{
    /// <summary>
    /// Возвращает тип пользователя: Курьер.
    /// </summary>
    public static readonly Roles Courier = new(new Guid("2cae9a9e-ebf4-44f3-b7d6-70d76444da72"), new RolesCode("courier"), new RolesName("Курьер"));

    /// <summary>
    /// Возвращает тип пользователя: Пользователь.
    /// </summary>
    public static readonly Roles BasicUser = new(new Guid("14b00673-ed23-4f3e-880a-2aa1d913f8b2"), new RolesCode("basicUser"), new RolesName("Пользователь"));
    /// <summary>
    /// Возвращает тип пользователя: Неавторизованный пользователь.
    /// </summary>
    public static readonly Roles NotAuthUser = new(new Guid("5397baa2-61a2-408b-966d-09c451440bb2"), new RolesCode("notAuthUser"), new RolesName("Неавторизованный пользователь"));

    private static readonly Dictionary<RolesCode, Roles> RolesNames = new()
    {
        [Courier.Code] = Courier,
        [BasicUser.Code] = BasicUser,
        [NotAuthUser.Code] = NotAuthUser
    };
    
    
    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="Roles" />.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="code">Код.</param>
    /// <param name="name">Наименование.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="code" /> или
    /// <paramref name="name" /> равен <c>null</c>.
    /// </exception>
    /// <remarks>Конструктор для EF.</remarks>
    protected Roles (SequentialGuid id, RolesCode code, RolesName name)
        : base(id)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    /// <summary>
    /// Возвращает код.
    /// </summary>
    public RolesCode Code { get; }

    /// <summary>
    /// Возвращает наименование.
    /// </summary>
    public RolesName Name { get; }

    /// <summary>
    /// Возвращает все значения перечисления.
    /// </summary>
    public static IReadOnlyCollection<Roles> GetAllValues() => RolesNames.Values;
}
