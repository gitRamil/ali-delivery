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
    /// <param name="passportId">Номер паспорта пользователя.</param>
    /// <param name="roleId">Идентификатор роли пользователя.</param>
    /// <param name="birthday">Дата рождения пользователя.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если любой из параметров <paramref name="firstName" />, 
    /// <paramref name="lastName" />, <paramref name="passportId" />, 
    /// <paramref name="roleId" /> или <paramref name="birthday" /> равен <c>null</c>.
    /// </exception>
    public User(
        SequentialGuid id, 
        FirstName firstName, 
        LastName lastName, 
        PassportId passportId, 
        RoleId roleId,  
        Birthday birthday)
        : base(id)
    {
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        PassportId = passportId ?? throw new ArgumentNullException(nameof(passportId));
        RoleId = roleId ?? throw new ArgumentNullException(nameof(roleId));
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
        PassportId = null!;
        Birthday = null!;
        RoleId = null!;
    }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public FirstName FirstName { get;  }

    /// <summary>
    /// Фамилия пользователя.
    /// </summary>
    public LastName LastName { get;  }

    /// <summary>
    /// Номер паспорта пользователя.
    /// </summary>
    public PassportId PassportId { get;   }

    /// <summary>
    /// Идентификатор роли пользователя.
    /// </summary>
    public virtual RoleId RoleId { get;   }

    /// <summary>
    /// Дата рождения пользователя.
    /// </summary>
    public Birthday Birthday { get;  }
}