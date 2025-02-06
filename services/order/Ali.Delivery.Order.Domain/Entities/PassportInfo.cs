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
    /// <param name="passportInfoPassportNumber">Номер паспорта.</param>
    /// <param name="passportInfoRegDate">Дата регистрации паспорта.</param>
    /// <param name="passportInfoIssuedBy">Кем выдан.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если любой из параметров <paramref name="typeId" />, <paramref name="passportInfoPassportNumber" />,
    /// <paramref name="passportInfoRegDate" /> или <paramref name="passportInfoIssuedBy" /> равен <c>null</c>.
    /// </exception>
    public PassportInfo(SequentialGuid id,
                        PassportType typeId,
                        PassportInfoPassportNumber passportInfoPassportNumber,
                        PassportInfoRegDate passportInfoRegDate,
                        PassportInfoIssuedBy passportInfoIssuedBy)
        : base(id)
    {
        PassportType = typeId ?? throw new ArgumentNullException(nameof(typeId));
        PassportInfoPassportNumber = passportInfoPassportNumber ?? throw new ArgumentNullException(nameof(passportInfoPassportNumber));
        PassportInfoRegDate = passportInfoRegDate ?? throw new ArgumentNullException(nameof(passportInfoRegDate));
        PassportInfoIssuedBy = passportInfoIssuedBy ?? throw new ArgumentNullException(nameof(passportInfoIssuedBy));
    }

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="PassportInfo" />.
    /// </summary>
    /// <remarks>Конструктор для EF.</remarks>
    protected PassportInfo()
        : base(SequentialGuid.Empty)
    {
        PassportType = null!;
        PassportInfoPassportNumber = null!;
        PassportInfoRegDate = null!;
        PassportInfoIssuedBy  = null!;
    }

    /// <summary>
    /// Возвращает строку "кем выдан".
    /// </summary>
    public virtual PassportInfoIssuedBy PassportInfoIssuedBy{ get; set; }

    /// <summary>
    /// Возвращает номер паспорта.
    /// </summary>
    public virtual PassportInfoPassportNumber PassportInfoPassportNumber { get; set; }

    /// <summary>
    /// Возвращает дату регистрации паспорта.
    /// </summary>
    public virtual PassportInfoRegDate PassportInfoRegDate { get; set; }

    /// <summary>
    /// Возвращает идентификатор типа паспорта.
    /// </summary>
    public virtual PassportType PassportType { get; set; }
}
