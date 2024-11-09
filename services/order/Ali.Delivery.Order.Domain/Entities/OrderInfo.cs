using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.OrderInfo;

namespace Ali.Delivery.Order.Domain.Entities;

/// <summary>
/// Представляет информацию о заказе.
/// </summary>
public class OrderInfo : Entity<SequentialGuid>
{
    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="OrderInfo" />.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="weight">Вес заказа.</param>
    /// <param name="size">Размер заказа.</param>
    /// <param name="price">Цена заказа.</param>
    /// <param name="addressFrom">Адрес отправления.</param>
    /// <param name="addressTo">Адрес доставки.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если любой из параметров <paramref name="weight" />, <paramref name="size" />,
    /// <paramref name="price" />, <paramref name="addressFrom" /> или <paramref name="addressTo" /> равен <c>null</c>.
    /// </exception>
    public OrderInfo(SequentialGuid id, Weight weight, Size size, Price price, AddressFrom addressFrom, AddressTo addressTo)
        : base(id)
    {
        Weight = weight ?? throw new ArgumentNullException(nameof(weight));
        Size = size ?? throw new ArgumentNullException(nameof(size));
        Price = price ?? throw new ArgumentNullException(nameof(price));
        AddressFrom = addressFrom ?? throw new ArgumentNullException(nameof(addressFrom));
        AddressTo = addressTo ?? throw new ArgumentNullException(nameof(addressTo));
    }

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="OrderInfo" />.
    /// </summary>
    /// <remarks>Конструктор для EF.</remarks>
    protected OrderInfo()
        : base(SequentialGuid.Empty)
    {
        Weight = null!;
        Size = null!;
        Price = null!;
        AddressFrom = null!;
        AddressTo = null!;
    }

    /// <summary>
    /// Адрес отправления.
    /// </summary>
    public AddressFrom AddressFrom { get; }

    /// <summary>
    /// Адрес доставки.
    /// </summary>
    public AddressTo AddressTo { get; }

    /// <summary>
    /// Цена заказа.
    /// </summary>
    public Price Price { get; }

    /// <summary>
    /// Размер заказа.
    /// </summary>
    public virtual Size Size { get; }

    /// <summary>
    /// Вес заказа.
    /// </summary>
    public Weight Weight { get; }
}
