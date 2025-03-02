using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Domain.ValueObjects.NotAuthUser;

namespace Ali.Delivery.Order.Domain.Entities;

/// <summary>
/// Представляет сущность незарегистрированного пользователя.
/// </summary>
public class NotAuthUser : Entity<SequentialGuid>
{
    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="NotAuthUser" />.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="creator">Создатель.</param>
    /// <param name="firstName">Имя.</param>
    /// <param name="lastName">Фамилия.</param>
    /// <param name="phoneNumber">Телефонный номер.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="creator" /> равен <c>null</c>.
    /// </exception>
    public NotAuthUser(SequentialGuid id, User creator, NotAuthUserFirstName? firstName, NotAuthUserLastName? lastName, NotAuthUserPhoneNumber? phoneNumber)
        : base(id)
    {
        Creator = creator ?? throw new ArgumentNullException(nameof(creator));
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
    }

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="NotAuthUser" />.
    /// </summary>
    /// <remarks>Конструктор для Entity Framework.</remarks>
    protected NotAuthUser()
        : base(SequentialGuid.Empty) =>
        Creator = null!;

    /// <summary>
    /// Создатель.
    /// </summary>
    public virtual User Creator { get; init; }

    /// <summary>
    /// Имя.
    /// </summary>
    public NotAuthUserFirstName? FirstName { get; init; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    public NotAuthUserLastName? LastName { get; init; }

    /// <summary>
    /// Телефонный номер.
    /// </summary>
    public NotAuthUserPhoneNumber? PhoneNumber { get; init; }
}
