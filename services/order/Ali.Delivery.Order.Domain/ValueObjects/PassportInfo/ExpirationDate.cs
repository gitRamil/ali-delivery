using System.Diagnostics;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.PassportInfo;

/// <summary>
/// Представляет дату истечения срока действия паспорта.
/// </summary>
[DebuggerDisplay("{_expirationDate}")]
public sealed class ExpirationDate : ValueObject
{
    private readonly DateTime _expirationDate;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="ExpirationDate" />.
    /// </summary>
    /// <param name="expirationDate">Дата истечения срока действия паспорта.</param>
    public ExpirationDate(DateTime expirationDate)
    {
        _expirationDate = expirationDate;
    }

    /// <inheritdoc />
    public override string ToString() => _expirationDate.ToString("yyyy-MM-dd");

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _expirationDate;
    }

    public static implicit operator DateTime(ExpirationDate obj) => obj._expirationDate;
}