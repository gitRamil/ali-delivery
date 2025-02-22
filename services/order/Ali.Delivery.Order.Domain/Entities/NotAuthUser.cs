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
    /// Возникает, если любой из параметров <paramref name="firstName" />,
    /// <paramref name="lastName" />  равен <c>null</c>.
    /// </exception>
    public NotAuthUser(SequentialGuid id, User creator, NotAuthUserFirstName? firstName = null, NotAuthUserLastName? lastName = null, NotAuthUserPhoneNumber? phoneNumber = null)
        : base(id)
    {
        Creator = creator;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
    }

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="NotAuthUser" /> для использования ORM.
    /// </summary>
    /// <remarks>Конструктор без параметров необходим для Entity Framework.</remarks>
    protected NotAuthUser()
        : base(SequentialGuid.Empty)
    {
        Creator = null!;
        FirstName = null!;
        LastName = null!;
        PhoneNumber = null!;
    }

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
