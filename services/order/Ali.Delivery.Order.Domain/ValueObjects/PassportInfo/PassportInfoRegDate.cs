using System.Diagnostics;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.PassportInfo;

/// <summary>
/// Представляет дату регистрации паспорта.
/// </summary>
[DebuggerDisplay("{_regDate}")]
public sealed class PassportInfoRegDate : ValueObject
{
    private readonly DateTime _regDate;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="PassportInfoRegDate" />.
    /// </summary>
    /// <param name="regDate">Дата регистрации паспорта.</param>
    public PassportInfoRegDate(DateTime regDate) => _regDate = regDate;

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
    /// Выполняет неявное преобразование из <see cref="PassportInfoRegDate" /> в <see cref="DateTime" />.
    /// </summary>
    public static implicit operator DateTime(PassportInfoRegDate obj) => obj._regDate;
}
