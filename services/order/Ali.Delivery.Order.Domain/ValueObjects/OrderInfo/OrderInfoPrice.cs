using System.Diagnostics;
using System.Globalization;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.OrderInfo;

/// <summary>
/// Представляет цену заказа.
/// </summary>
[DebuggerDisplay("{_price}")]
public class OrderInfoPrice : ValueObject
{
    /// <summary>
    /// Минимальная допустимая цена заказа.
    /// </summary>
    private const decimal MinPrice = 0.01m;

    private readonly decimal _price;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="OrderInfoPrice" />.
    /// </summary>
    /// <param name="price">Цена заказа.</param>
    /// <exception cref="ArgumentException">Возникает, если цена меньше минимальной допустимой.</exception>
    public OrderInfoPrice(decimal price)
    {
        if (price < MinPrice)
        {
            throw new ArgumentException($"Цена заказа не может быть меньше {MinPrice} рублей.", nameof(price));
        }

        _price = price;
    }

    /// <inheritdoc />
    public override string ToString() => _price.ToString(CultureInfo.InvariantCulture);

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _price;
    }

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="OrderInfoPrice" /> в <see cref="decimal" />.
    /// </summary>
    public static implicit operator decimal(OrderInfoPrice obj) => obj._price;
}
