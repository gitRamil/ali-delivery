using System.Diagnostics;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.PassportInfo;

/// <summary>
/// Представляет дату регистрации паспорта.
/// </summary>
[DebuggerDisplay("{_regDate}")]
public sealed class RegDate : ValueObject
{
    private readonly DateTime _regDate;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="RegDate" />.
    /// </summary>
    /// <param name="regDate">Дата регистрации паспорта.</param>
    public RegDate(DateTime regDate)
    {
        _regDate = regDate;
    }

    /// <inheritdoc />
    public override string ToString() => _regDate.ToString("yyyy-MM-dd");

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _regDate;
    }

    public static implicit operator DateTime(RegDate obj) => obj._regDate;
}