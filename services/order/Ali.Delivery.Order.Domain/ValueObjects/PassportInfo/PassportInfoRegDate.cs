using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Ali.Delivery.Domain.Core;
using Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.OrderStatus;

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
    public PassportInfoRegDate(DateTime regDate)
    {
        if (regDate == default)
        {
            throw new ArgumentException("Дата регистрации не может быть значением по умолчанию.", nameof(regDate));
        }
        
        _regDate = regDate;
    }

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
    /// Выполняет явное преобразование из <see cref="DateTime" /> в <see cref="PassportInfoRegDate" />.
    /// </summary>
    /// <param name="obj">Значение наименования справочника статусов заказа.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static explicit operator PassportInfoRegDate?(DateTime? obj) => obj == null ? null : (PassportInfoRegDate)obj;

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="PassportInfoRegDate" /> в <see cref="DateTime" />.
    /// </summary>
    /// <param name="obj">Значение наименования справочника статусов заказа.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator DateTime?(PassportInfoRegDate? obj) => obj?._regDate;
}
