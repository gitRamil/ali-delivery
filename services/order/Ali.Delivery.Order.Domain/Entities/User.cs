using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.User;

namespace Ali.Delivery.Order.Domain.Entities;

/// <summary>
/// Представляет сущность пользователя.
/// </summary>
public class User : Entity<SequentialGuid>
{
    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="User" />.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="userFirstName">Имя пользователя.</param>
    /// <param name="userLastName">Фамилия пользователя.</param>
    /// <param name="passportInfo">Информация о паспорте пользователя</param>
    /// <param name="role">Идентификатор роли пользователя.</param>
    /// <param name="userBirthDay">Дата рождения пользователя.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если любой из параметров <paramref name="userFirstName" />,
    /// <paramref name="userLastName" />, <paramref name="passportInfo" />,
    /// <paramref name="role" /> или <paramref name="userBirthDay" /> равен <c>null</c>.
    /// </exception>
    public User(SequentialGuid id, UserFirstName userFirstName, UserLastName userLastName, PassportInfo passportInfo, Role role, UserBirthDay userBirthDay)
        : base(id)
    {
        UserFirstName = userFirstName ?? throw new ArgumentNullException(nameof(userFirstName));
        UserLastName = userLastName ?? throw new ArgumentNullException(nameof(userLastName));
        PassportInfo = passportInfo ?? throw new ArgumentNullException(nameof(passportInfo));
        Role = role ?? throw new ArgumentNullException(nameof(role));
        UserBirthDay = userBirthDay ?? throw new ArgumentNullException(nameof(userBirthDay));
    }

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="User" /> для использования ORM.
    /// </summary>
    /// <remarks>Конструктор без параметров необходим для Entity Framework.</remarks>
    protected User()
        : base(SequentialGuid.Empty)
    {
        UserFirstName = null!;
        UserLastName = null!;
        PassportInfo = null!;
        UserBirthDay = null!;
        Role = null!;
    }

    /// <summary>
    /// Информация о паспорте пользователя.
    /// </summary>
    public virtual PassportInfo PassportInfo { get; }

    /// <summary>
    /// Идентификатор роли пользователя.
    /// </summary>
    public virtual Role Role { get; }

    /// <summary>
    /// Дата рождения пользователя.
    /// </summary>
    public UserBirthDay UserBirthDay { get; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public UserFirstName UserFirstName { get; }

    /// <summary>
    /// Фамилия пользователя.
    /// </summary>
    public UserLastName UserLastName { get; }
}
