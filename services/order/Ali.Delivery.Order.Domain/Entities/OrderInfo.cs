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
    /// <param name="weight">Вес.</param>
    /// <param name="size">Размер.</param>
    /// <param name="price">Цена.</param>
    /// <param name="addressFrom">Адрес отправления.</param>
    /// <param name="addressTo">Адрес доставки.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="weight" /> или
    /// <paramref name="size" /> или
    /// <paramref name="price" /> или
    /// <paramref name="addressFrom" /> или
    /// <paramref name="addressTo" /> равен <c>null</c>.
    /// </exception>
    public OrderInfo(SequentialGuid id, OrderInfoWeight weight, Size size, OrderInfoPrice price, OrderInfoAddressFrom addressFrom, OrderInfoAddressTo addressTo)
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
    public OrderInfoAddressFrom AddressFrom { get; private set; }

    /// <summary>
    /// Адрес доставки.
    /// </summary>
    public OrderInfoAddressTo AddressTo { get; private set; }

    /// <summary>
    /// Цена.
    /// </summary>
    public OrderInfoPrice Price { get; private set; }

    /// <summary>
    /// Размер.
    /// </summary>
    public virtual Size Size { get; private set; }

    /// <summary>
    /// Вес.
    /// </summary>
    public OrderInfoWeight Weight { get; private set; }

    /// <summary>
    /// Обновляет информацию заказа.
    /// </summary>
    /// <inheritdoc cref="OrderInfo" />
    public void UpdateOrderInfo(OrderInfoWeight weight, OrderInfoPrice price, OrderInfoAddressFrom addressFrom, OrderInfoAddressTo addressTo, Size size)
    {
        Weight = weight;
        Price = price;
        AddressFrom = addressFrom;
        AddressTo = addressTo;
        Size = size;
    }
}
