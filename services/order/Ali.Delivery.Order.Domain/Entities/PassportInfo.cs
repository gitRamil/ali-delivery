using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.PassportInfo;

namespace Ali.Delivery.Order.Domain.Entities;

/// <summary>
/// Представляет паспортную информацию пользователя.
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
    /// Возникает, если любой из параметров <paramref name="typeId" />, <paramref name="passportNumber" />,
    /// <paramref name="regDate" /> или <paramref name="expirationDate" /> равен <c>null</c>.
    /// </exception>
    public PassportInfo(SequentialGuid id, PassportType typeId, PassportNumber passportNumber, RegDate regDate, ExpirationDate expirationDate)
        : base(id)
    {
        PassportType = typeId ?? throw new ArgumentNullException(nameof(typeId));
        PassportNumber = passportNumber ?? throw new ArgumentNullException(nameof(passportNumber));
        RegDate = regDate ?? throw new ArgumentNullException(nameof(regDate));
        ExpirationDate = expirationDate ?? throw new ArgumentNullException(nameof(expirationDate));
    }

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="PassportInfo" />.
    /// </summary>
    /// <remarks>Конструктор для EF.</remarks>
    protected PassportInfo()
        : base(SequentialGuid.Empty)
    {
        PassportType = null!;
        PassportNumber = null!;
        RegDate = null!;
        ExpirationDate = null!;
    }

    /// <summary>
    /// Возвращает дату окончания действия паспорта.
    /// </summary>
    public ExpirationDate ExpirationDate { get; }

    /// <summary>
    /// Возвращает номер паспорта.
    /// </summary>
    public PassportNumber PassportNumber { get; }

    /// <summary>
    /// Возвращает идентификатор типа паспорта.
    /// </summary>
    public virtual PassportType PassportType { get; }

    /// <summary>
    /// Возвращает дату регистрации паспорта.
    /// </summary>
    public RegDate RegDate { get; }
}
