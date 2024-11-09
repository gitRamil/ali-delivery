using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.User;

namespace Ali.Delivery.Order.Domain.Entities;

public class User : Entity<SequentialGuid>
{
    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="User" />.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="firstName">Имя пользователя.</param>
    /// <param name="lastName">Фамилия пользователя.</param>
    /// <param name="passportInfo">Информация о паспорте пользователя</param>
    /// <param name="role">Идентификатор роли пользователя.</param>
    /// <param name="birthday">Дата рождения пользователя.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если любой из параметров <paramref name="firstName" />,
    /// <paramref name="lastName" />, <paramref name="passportInfo" />,
    /// <paramref name="role" /> или <paramref name="birthday" /> равен <c>null</c>.
    /// </exception>
    public User(SequentialGuid id, FirstName firstName, LastName lastName, PassportInfo passportInfo, Role role, Birthday birthday)
        : base(id)
    {
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        PassportInfo = passportInfo ?? throw new ArgumentNullException(nameof(passportInfo));
        Role = role ?? throw new ArgumentNullException(nameof(role));
        Birthday = birthday ?? throw new ArgumentNullException(nameof(birthday));
    }

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="User" /> для использования ORM.
    /// </summary>
    /// <remarks>Конструктор без параметров необходим для Entity Framework.</remarks>
    protected User()
        : base(SequentialGuid.Empty)
    {
        FirstName = null!;
        LastName = null!;
        PassportInfo = null!;
        Birthday = null!;
        Role = null!;
    }

    /// <summary>
    /// Дата рождения пользователя.
    /// </summary>
    public Birthday Birthday { get; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public FirstName FirstName { get; }

    /// <summary>
    /// Фамилия пользователя.
    /// </summary>
    public LastName LastName { get; }

    /// <summary>
    /// Информация о паспорте пользователя.
    /// </summary>
    public virtual PassportInfo PassportInfo { get; }

    /// <summary>
    /// Идентификатор роли пользователя.
    /// </summary>
    public virtual Role Role { get; }
}
