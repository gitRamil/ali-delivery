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
    /// <param name="login">Логин.</param>
    /// <param name="password">Пароль.</param>
    /// <param name="userFirstName">Имя пользователя.</param>
    /// <param name="userLastName">Фамилия пользователя.</param>
    /// <param name="role">Идентификатор роли пользователя.</param>
    /// <param name="userBirthDay">Дата рождения пользователя.</param>
    /// <param name="passportInfo">Информация о паспорте пользователя</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если любой из параметров <paramref name="userFirstName" />,
    /// <paramref name="userLastName" />, <paramref name="passportInfo" />,
    /// <paramref name="role" /> или <paramref name="userBirthDay" /> равен <c>null</c>.
    /// </exception>
    public User(SequentialGuid id,
                UserLogin login,
                UserPassword password,
                UserFirstName userFirstName,
                UserLastName userLastName,
                Role role,
                UserBirthDay userBirthDay,
                PassportInfo? passportInfo = null)
        : base(id)
    {
        Login = login ?? throw new ArgumentNullException(nameof(login));
        Password = password ?? throw new ArgumentNullException(nameof(password));
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
        Login = null!;
        Password = null!;
        UserFirstName = null!;
        UserLastName = null!;
        PassportInfo = null!;
        UserBirthDay = null!;
        Role = null!;
    }

    /// <summary>
    /// Логин пользователя.
    /// </summary>
    public UserLogin Login { get; private set; }

    /// <summary>
    /// Информация о паспорте пользователя.
    /// </summary>
    public virtual PassportInfo PassportInfo { get; set; }

    /// <summary>
    /// Пароль пользователя.
    /// </summary>
    public UserPassword Password { get; private set; }

    /// <summary>
    /// Идентификатор роли пользователя.
    /// </summary>
    public virtual Role Role { get; private set; }

    /// <summary>
    /// Дата рождения пользователя.
    /// </summary>
    public UserBirthDay UserBirthDay { get; private set; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public UserFirstName UserFirstName { get; private set; }

    /// <summary>
    /// Фамилия пользователя.
    /// </summary>
    public UserLastName UserLastName { get; private set; }

    /// <summary>
    /// Обновляет дату рождения пользователя.
    /// </summary>
    /// <param name="birthDay">Новая дата рождения.</param>
    public void UpdateBirthDay(UserBirthDay birthDay)
    {
        UserBirthDay = birthDay ?? throw new ArgumentNullException(nameof(birthDay));
    }

    /// <summary>
    /// Обновляет имя и фамилию пользователя.
    /// </summary>
    /// <param name="userFirstName">Новое имя пользователя.</param>
    /// <param name="userLastName">Новая фамилия пользователя.</param>
    public void UpdateName(UserFirstName userFirstName, UserLastName userLastName)
    {
        UserFirstName = userFirstName ?? throw new ArgumentNullException(nameof(userFirstName));
        UserLastName = userLastName ?? throw new ArgumentNullException(nameof(userLastName));
    }

    /// <summary>
    /// Обновляет роль пользователя.
    /// </summary>
    /// <param name="role">Новая роль.</param>
    public void UpdateRole(Role role)
    {
        Role = role ?? throw new ArgumentNullException(nameof(role));
    }
}
