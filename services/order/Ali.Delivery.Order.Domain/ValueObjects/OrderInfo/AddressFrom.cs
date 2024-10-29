using System.Diagnostics;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.OrderInfo;

/// <summary>
/// Представляет адрес отправления заказа.
/// </summary>
[DebuggerDisplay("{_address}")]
public class AddressFrom : ValueObject
{
    /// <summary>
    /// Максимальная длина адреса.
    /// </summary>
    public const int MaxLength = 250;

    private readonly string _address;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="AddressFrom" />.
    /// </summary>
    /// <param name="address">Адрес отправления заказа.</param>
    /// <exception cref="ArgumentException">Возникает, если адрес пуст или превышает максимальную длину.</exception>
    public AddressFrom(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
        {
            throw new ArgumentException("Адрес не может быть пустым.", nameof(address));
        }

        if (address.Length > MaxLength)
        {
            throw new ArgumentException($"Адрес не может быть длиннее {MaxLength} символов.", nameof(address));
        }

        _address = address;
    }

    /// <inheritdoc />
    public override string ToString() => _address;

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _address;
    }
    
    /// <summary>
    /// Выполняет неявное преобразование из <see cref="AddressFrom" /> в <see cref="string" />.
    /// </summary>
    public static implicit operator string(AddressFrom obj) => obj._address;
}
