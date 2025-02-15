using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.PassportInfo;
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
    /// <param name="firstName">Имя пользователя.</param>
    /// <param name="lastName">Фамилия пользователя.</param>
    /// <param name="role">Идентификатор роли пользователя.</param>
    /// <param name="birthDay">Дата рождения пользователя.</param>
    /// <param name="passportInfo">Информация о паспорте пользователя</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если любой из параметров <paramref name="firstName" />,
    /// <paramref name="lastName" />, <paramref name="passportInfo" />,
    /// <paramref name="role" /> или <paramref name="birthDay" /> равен <c>null</c>.
    /// </exception>
    public User(SequentialGuid id,
                UserLogin login,
                UserPassword password,
                Role role,
                UserBirthDay birthDay,
                UserFirstName? firstName = null,
                UserLastName? lastName = null,
                PassportInfo? passportInfo = null)
        : base(id)
    {
        Login = login ?? throw new ArgumentNullException(nameof(login));
        Password = password ?? throw new ArgumentNullException(nameof(password));
        FirstName = firstName;
        LastName = lastName;
        PassportInfo = passportInfo;
        Role = role ?? throw new ArgumentNullException(nameof(role));
        BirthDay = birthDay ?? throw new ArgumentNullException(nameof(birthDay));
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
        FirstName = null!;
        LastName = null!;
        PassportInfo = null!;
        BirthDay = null!;
        Role = null!;
    }

    /// <summary>
    /// Логин пользователя.
    /// </summary>
    public UserLogin Login { get; private set; }

    /// <summary>
    /// Информация о паспорте пользователя.
    /// </summary>
    public virtual PassportInfo? PassportInfo { get; private set; }

    /// <summary>
    /// Пароль.
    /// </summary>
    public UserPassword Password { get; private set; }

    /// <summary>
    /// Идентификатор роли.
    /// </summary>
    public virtual Role Role { get; private set; }

    /// <summary>
    /// Дата рождения.
    /// </summary>
    public UserBirthDay BirthDay { get; private set; }

    /// <summary>
    /// Имя.
    /// </summary>
    public UserFirstName? FirstName { get; private set; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    public UserLastName? LastName { get; private set; }

    /// <summary>
    /// Создаёт паспортную информацию для пользователя.
    /// </summary>
    /// <param name="passportId">Идентификатор паспорта.</param>
    /// <param name="typeId">Тип паспорта.</param>
    /// <param name="passportNumber">Номер паспорта.</param>
    /// <param name="regDate">Дата регистрации.</param>
    /// <param name="issuedBy">Кем выдан.</param>
    /// <exception cref="InvalidOperationException">Выбрасывается, если у пользователя уже есть паспорт.</exception>
    public void CreatePassportInfo(SequentialGuid passportId,
                                   PassportType typeId,
                                   PassportInfoPassportNumber passportNumber,
                                   PassportInfoRegDate regDate,
                                   PassportInfoIssuedBy issuedBy)
    {
        if (PassportInfo is not null)
        {
            throw new InvalidOperationException("Паспортные данные уже существуют.");
        }

        PassportInfo = new PassportInfo(passportId, typeId, passportNumber, regDate, issuedBy);
    }

    /// <summary>
    /// Обновляет дату рождения.
    /// </summary>
    /// <param name="birthDay">Новая дата рождения.</param>
    public void UpdateBirthDay(UserBirthDay birthDay)
    {
        BirthDay = birthDay ?? throw new ArgumentNullException(nameof(birthDay));
    }

    /// <summary>
    /// Обновляет логин.
    /// </summary>
    /// <param name="login">Новый логин.</param>
    public void UpdateLogin(UserLogin login)
    {
        Login = login ?? throw new ArgumentNullException(nameof(login));
    }

    /// <summary>
    /// Обновляет имя и фамилию.
    /// </summary>
    /// <param name="userFirstName">Новое имя.</param>
    /// <param name="userLastName">Новая фамилия.</param>
    public void UpdateName(UserFirstName userFirstName, UserLastName userLastName)
    {
        FirstName = userFirstName ?? throw new ArgumentNullException(nameof(userFirstName));
        LastName = userLastName ?? throw new ArgumentNullException(nameof(userLastName));
    }

    /// <summary>
    /// Обновляет роль.
    /// </summary>
    /// <param name="role">Новая роль.</param>
    public void UpdateRole(Role role)
    {
        Role = role ?? throw new ArgumentNullException(nameof(role));
    }
}
