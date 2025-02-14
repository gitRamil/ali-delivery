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
    /// <param name="id">Идентификатор незарегистрированного пользователя.</param>
    /// <param name="notAuthUserFirstName">Имя незарегистрированного пользователя.</param>
    /// <param name="notAuthUserLastName">Фамилия незарегистрированного пользователя.</param>
    /// <param name="notAuthUserPhoneNumber">Телефонный номер незарегистрированного пользователя.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если любой из параметров <paramref name="notAuthUserFirstName" />,
    /// <paramref name="notAuthUserLastName" />  равен <c>null</c>.
    /// </exception>
    public NotAuthUser(SequentialGuid id,
                       NotAuthUserFirstName? notAuthUserFirstName = null,
                       NotAuthUserLastName? notAuthUserLastName = null,
                       NotAuthUserPhoneNumber? notAuthUserPhoneNumber = null)
        : base(id)
    {
        NotAuthUserFirstName = notAuthUserFirstName;
        NotAuthUserLastName = notAuthUserLastName;
        NotAuthPhoneNumber = notAuthUserPhoneNumber;
        
    }

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="NotAuthUser" /> для использования ORM.
    /// </summary>
    /// <remarks>Конструктор без параметров необходим для Entity Framework.</remarks>
    protected NotAuthUser()
        : base(SequentialGuid.Empty)
    {
        NotAuthUserFirstName = null!;
        NotAuthUserLastName = null!;
        NotAuthPhoneNumber = null!;
    }
    
    /// <summary>
    /// Телефонный номер незарегистрированного пользователя.
    /// </summary>
    public virtual NotAuthUserPhoneNumber? NotAuthPhoneNumber { get; set; }

    /// <summary>
    /// Имя незарегистрированного пользователя.
    /// </summary>
    public virtual NotAuthUserFirstName? NotAuthUserFirstName { get; set; }

    /// <summary>
    /// Фамилия незарегистрированного пользователя.
    /// </summary>
    public virtual NotAuthUserLastName? NotAuthUserLastName { get; set; }
    
}