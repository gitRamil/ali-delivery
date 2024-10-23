using System.Diagnostics;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.OrderInfo;

/// <summary>
/// Представляет адрес доставки заказа.
/// </summary>
[DebuggerDisplay("{_address}")]
public class AddressTo : ValueObject
{
    /// <summary>
    /// Максимальная длина адреса.
    /// </summary>
    public const int MaxLength = 250;

    private readonly string _address;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="AddressTo" />.
    /// </summary>
    /// <param name="address">Адрес доставки заказа.</param>
    /// <exception cref="ArgumentException">Возникает, если адрес пуст или превышает максимальную длину.</exception>
    public AddressTo(string address)
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

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _address;
    }

    public static implicit operator string(AddressTo obj) => obj._address;
}