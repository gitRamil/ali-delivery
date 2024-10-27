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

    public Birthday(DateTime birthDate)
    {
        if (birthDate == default)
        {
            throw new ArgumentException("Дата рождения не может быть значением по умолчанию.", nameof(birthDate));
        }

        _birthDate = birthDate;
    }

    public override string ToString() => _birthDate.ToShortDateString();

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _birthDate;
    }

    public static implicit operator DateTime(Birthday obj) => obj._birthDate;
}
