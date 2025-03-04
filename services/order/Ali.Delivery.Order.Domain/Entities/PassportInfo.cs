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
    /// <param name="issuedBy">Кем выдан.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="typeId" /> или
    /// <paramref name="passportNumber" /> или
    /// <paramref name="regDate" /> или
    /// <paramref name="issuedBy" /> равен <c>null</c>.
    /// </exception>
    public PassportInfo(SequentialGuid id, PassportType typeId, PassportInfoPassportNumber passportNumber, PassportInfoRegDate regDate, PassportInfoIssuedBy issuedBy)
        : base(id)
    {
        PassportType = typeId ?? throw new ArgumentNullException(nameof(typeId));
        PassportNumber = passportNumber ?? throw new ArgumentNullException(nameof(passportNumber));
        RegDate = regDate ?? throw new ArgumentNullException(nameof(regDate));
        IssuedBy = issuedBy ?? throw new ArgumentNullException(nameof(issuedBy));
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
        IssuedBy = null!;
    }

    /// <summary>
    /// Возвращает строку "кем выдан".
    /// </summary>
    public PassportInfoIssuedBy IssuedBy { get; set; }

    /// <summary>
    /// Возвращает номер паспорта.
    /// </summary>
    public PassportInfoPassportNumber PassportNumber { get; set; }

    /// <summary>
    /// Возвращает идентификатор типа паспорта.
    /// </summary>
    public virtual PassportType PassportType { get; set; }

    /// <summary>
    /// Возвращает дату регистрации паспорта.
    /// </summary>
    public PassportInfoRegDate RegDate { get; set; }

    /// <summary>
    /// Обновляет данные паспорта.
    /// </summary>
    /// <param name="typeId">Тип паспорта.</param>
    /// <param name="passportNumber">Номер паспорта.</param>
    /// <param name="regDate">Дата регистрации.</param>
    /// <param name="issuedBy">Кем выдан.</param>
    /// <exception cref="InvalidOperationException"></exception>
    public void UpdatePassport(PassportInfoPassportNumber passportNumber, PassportInfoRegDate regDate, PassportInfoIssuedBy issuedBy, PassportType typeId)
    {
        PassportType = typeId ?? throw new ArgumentNullException(nameof(typeId));
        PassportNumber = passportNumber ?? throw new ArgumentNullException(nameof(passportNumber));
        RegDate = regDate ?? throw new ArgumentNullException(nameof(regDate));
        IssuedBy = issuedBy ?? throw new ArgumentNullException(nameof(issuedBy));
    }
}
