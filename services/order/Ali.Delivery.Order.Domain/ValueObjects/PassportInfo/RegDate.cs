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
    public RegDate(DateTime regDate) => _regDate = regDate;

    /// <inheritdoc />
    public override string ToString() => _regDate.ToString("yyyy-MM-dd");

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _regDate;
    }

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="RegDate" /> в <see cref="DateTime" />.
    /// </summary>
    public static implicit operator DateTime(RegDate obj) => obj._regDate;
}
