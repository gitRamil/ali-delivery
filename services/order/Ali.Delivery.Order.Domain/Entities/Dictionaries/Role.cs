using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.Role;

namespace Ali.Delivery.Order.Domain.Entities.Dictionaries;

/// <summary>
/// Представляет роли пользователей.
/// </summary>
public class Role : Entity<SequentialGuid>
{
    /// <summary>
    /// Возвращает тип пользователя: Пользователь.
    /// </summary>
    public static readonly Role BasicUser = new(new Guid("3a1844b2-1e4f-f70c-c599-5fe824c7a873"), new RoleCode("basicUser"), new RoleName("Пользователь"));

    /// <summary>
    /// Возвращает тип пользователя: Курьер.
    /// </summary>
    public static readonly Role Courier = new(new Guid("3a1844b2-1e60-6e25-1bcf-da74b2a4f07c"), new RoleCode("courier"), new RoleName("Курьер"));

    /// <summary>
    /// Возвращает тип пользователя: Неавторизованный пользователь.
    /// </summary>
    public static readonly Role NotAuthUser = new(new Guid("3a1844b2-1e61-d7b0-a9f6-8ec08e2c69fd"), new RoleCode("notAuthUser"), new RoleName("Неавторизованный пользователь"));

    /// <summary>
    /// Возвращает тип пользователя: Пользователь с полным доступом.
    /// </summary>
    public static readonly Role SuperUser = new(new Guid("3a1844b2-1e63-f523-2f04-f4eb3bad17b3"), new RoleCode("superUser"), new RoleName("Пользователь с полным доступом"));

    private static readonly Dictionary<RoleCode, Role> RoleNames = new()
    {
        [Courier.Code] = Courier,
        [BasicUser.Code] = BasicUser,
        [NotAuthUser.Code] = NotAuthUser,
        [SuperUser.Code] = SuperUser
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
    public Role(SequentialGuid id, RoleCode code, RoleName name)
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
    /// Определяет, является ли роль системной (например, SuperUser).
    /// </summary>
    public bool IsSystemRole() => this == SuperUser;

    /// <summary>
    /// Возвращает все значения перечисления.
    /// </summary>
    public static IReadOnlyCollection<Role> GetAllValues() => RoleNames.Values;
}
