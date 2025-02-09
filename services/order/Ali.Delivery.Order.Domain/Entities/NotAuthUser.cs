using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.NotAuthUser;

namespace Ali.Delivery.Order.Domain.Entities;

/// <summary>
/// Представляет сущность незарегистрированного пользователя.
/// </summary>
public class NotAuthUser : Entity<SequentialGuid>
{
    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="NotAuthUser" />.
    /// </summary>
    /// <param name="id">Идентификатор незарегистрированного пользователя.</param>
    /// <param name="creator">Создатель незарегистрированного пользователя.</param>
    /// <param name="notAuthUserFirstName">Имя незарегистрированного пользователя.</param>
    /// <param name="notAuthUserLastName">Фамилия незарегистрированного пользователя.</param>
    /// <param name="role">Идентификатор роли незарегистрированного пользователя.</param>
    /// <param name="notAuthUserPhoneNumber">Телефонный номер незарегистрированного пользователя.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если любой из параметров <paramref name="notAuthUserFirstName" />,
    /// <paramref name="notAuthUserLastName" /> или <paramref name="role" />  равен <c>null</c>.
    /// </exception>
    public NotAuthUser(SequentialGuid id, Role role, User creator, NotAuthUserFirstName? notAuthUserFirstName = null, NotAuthUserLastName? notAuthUserLastName = null, NotAuthUserPhoneNumber? notAuthUserPhoneNumber = null )
        : base(id)
    {
        NotAuthUserFirstName = notAuthUserFirstName;
        NotAuthUserLastName = notAuthUserLastName;
        Role = role ?? throw new ArgumentNullException(nameof(role));
        NotAuthPhoneNumber = notAuthUserPhoneNumber;
        Creator = creator ?? throw new ArgumentNullException(nameof(creator));
    }

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="NotAuthUser" /> для использования ORM.
    /// </summary>
    /// <remarks>Конструктор без параметров необходим для Entity Framework.</remarks>
    protected NotAuthUser()
        : base(SequentialGuid.Empty)
    {
        NotAuthUserFirstName = null!;
        NotAuthUserLastName  = null!;
        Role = null!;
        NotAuthPhoneNumber = null!;
        Creator = null!;
    }


    /// <summary>
    /// Идентификатор роли незарегистрированного пользователя.
    /// </summary>
    public virtual Role Role { get; private set; }

    /// <summary>
    /// Телефонный номер незарегистрированного пользователя.
    /// </summary>
    public virtual NotAuthUserPhoneNumber? NotAuthPhoneNumber { get;  set; }

    /// <summary>
    /// Имя незарегистрированного пользователя.
    /// </summary>
    public virtual NotAuthUserFirstName? NotAuthUserFirstName { get; set; }

    /// <summary>
    /// Фамилия незарегистрированного пользователя.
    /// </summary>
    public virtual NotAuthUserLastName? NotAuthUserLastName { get; set; }
    
    /// <summary>
    /// Возвращает создателя незарегистрированного пользователя.
    /// </summary>
    public virtual User? Creator { get; set; }
    
}

