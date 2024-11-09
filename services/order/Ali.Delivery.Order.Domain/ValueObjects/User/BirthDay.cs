using System.Diagnostics;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.User;

/// <summary>
/// Представляет дату рождения пользователя.
/// </summary>
[DebuggerDisplay("{_birthDate}")]
public class Birthday : ValueObject
{
    private readonly DateTime _birthDate;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="Birthday" /> с указанной датой рождения.
    /// </summary>
    /// <param name="birthDate">Дата рождения.</param>
    /// <exception cref="ArgumentException">
    /// Выбрасывается, если <paramref name="birthDate" /> имеет значение по умолчанию (01.01.0001).
    /// </exception>
    public Birthday(DateTime birthDate)
    {
        if (birthDate == default)
        {
            throw new ArgumentException("Дата рождения не может быть значением по умолчанию.", nameof(birthDate));
        }

        _birthDate = birthDate;
    }

    /// <inheritdoc />
    public override string ToString() => _birthDate.ToShortDateString();

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _birthDate;
    }

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="Birthday" /> в <see cref="DateTime" />.
    /// </summary>
    public static implicit operator DateTime(Birthday obj) => obj._birthDate;
}
