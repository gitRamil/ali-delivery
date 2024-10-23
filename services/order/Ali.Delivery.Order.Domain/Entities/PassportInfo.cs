using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.PassportInfo;

namespace Ali.Delivery.Order.Domain.Entities;

/// <summary>
/// Паспортная информация пользователя.
/// </summary>
public class PassportInfo : Entity<SequentialGuid>
{
    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="PassportInfo" />.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="typeId">Идентификатор типа паспорта.</param>
    /// <param name="passportNumber">Номер паспорта.</param>
    /// <param name="regDate">Дата регистрации паспорта.</param>
    /// <param name="expirationDate">Дата окончания действия паспорта.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="typeId" /> равен <c>null</c>.
    /// </exception>
    public PassportInfo(SequentialGuid id, PassportType typeId, PassportNumber passportNumber, RegDate regDate, ExpirationDate expirationDate)
        : base(id)
    {
        TypeId = typeId ?? throw new ArgumentNullException(nameof(typeId));
        PassportNumber = passportNumber ?? throw new ArgumentNullException(nameof(passportNumber));
        RegDate = regDate;
        ExpirationDate = expirationDate;
    }

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="PassportInfo" />.
    /// </summary>
    /// <remarks>Конструктор для EF.</remarks>
    protected PassportInfo()
        : base(SequentialGuid.Empty)
    {
        TypeId = null!;
        PassportNumber = null!;
        RegDate = null!;
        ExpirationDate = null!;
    }
    
    
    /// <summary>
    /// Возвращает идентификатор типа паспорта.
    /// </summary>
    public virtual PassportType TypeId { get; }

    /// <summary>
    /// Возвращает номер паспорта.
    /// </summary>
    public PassportNumber PassportNumber { get; }

    /// <summary>
    /// Возвращает дату регистрации паспорта.
    /// </summary>
    public RegDate RegDate { get; }

    /// <summary>
    /// Возвращает дату окончания действия паспорта.
    /// </summary>
    public ExpirationDate ExpirationDate { get; }
}