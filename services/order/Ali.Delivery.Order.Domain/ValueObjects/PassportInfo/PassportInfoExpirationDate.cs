using System.Diagnostics;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.PassportInfo;

/// <summary>
/// Представляет дату истечения срока действия паспорта.
/// </summary>
[DebuggerDisplay("{_expirationDate}")]
public sealed class PassportInfoExpirationDate : ValueObject
{
    private readonly DateTime _expirationDate;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="PassportInfoExpirationDate" />.
    /// </summary>
    /// <param name="expirationDate">Дата истечения срока действия паспорта.</param>
    public PassportInfoExpirationDate(DateTime expirationDate) => _expirationDate = expirationDate;

    /// <inheritdoc />
    public override string ToString() => _expirationDate.ToString("yyyy-MM-dd");

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _expirationDate;
    }

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="PassportInfoExpirationDate" /> в <see cref="DateTime" />.
    /// </summary>
    public static implicit operator DateTime(PassportInfoExpirationDate obj) => obj._expirationDate;
}
