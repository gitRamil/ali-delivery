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
    /// <param name="orderInfoWeight">Вес заказа.</param>
    /// <param name="size">Размер заказа.</param>
    /// <param name="orderInfoPrice">Цена заказа.</param>
    /// <param name="orderInfoAddressFrom">Адрес отправления.</param>
    /// <param name="orderInfoAddressTo">Адрес доставки.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если любой из параметров <paramref name="orderInfoWeight" />, <paramref name="size" />,
    /// <paramref name="orderInfoPrice" />, <paramref name="orderInfoAddressFrom" /> или <paramref name="orderInfoAddressTo" />
    /// равен <c>null</c>.
    /// </exception>
    public OrderInfo(SequentialGuid id,
                     OrderInfoWeight orderInfoWeight,
                     Size size,
                     OrderInfoPrice orderInfoPrice,
                     OrderInfoAddressFrom orderInfoAddressFrom,
                     OrderInfoAddressTo orderInfoAddressTo)
        : base(id)
    {
        OrderInfoWeight = orderInfoWeight ?? throw new ArgumentNullException(nameof(orderInfoWeight));
        Size = size ?? throw new ArgumentNullException(nameof(size));
        OrderInfoPrice = orderInfoPrice ?? throw new ArgumentNullException(nameof(orderInfoPrice));
        OrderInfoAddressFrom = orderInfoAddressFrom ?? throw new ArgumentNullException(nameof(orderInfoAddressFrom));
        OrderInfoAddressTo = orderInfoAddressTo ?? throw new ArgumentNullException(nameof(orderInfoAddressTo));
    }

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="OrderInfo" />.
    /// </summary>
    /// <remarks>Конструктор для EF.</remarks>
    protected OrderInfo()
        : base(SequentialGuid.Empty)
    {
        OrderInfoWeight = null!;
        Size = null!;
        OrderInfoPrice = null!;
        OrderInfoAddressFrom = null!;
        OrderInfoAddressTo = null!;
    }

    /// <summary>
    /// Адрес отправления.
    /// </summary>
    public virtual OrderInfoAddressFrom OrderInfoAddressFrom { get; set; }

    /// <summary>
    /// Адрес доставки.
    /// </summary>
    public virtual OrderInfoAddressTo OrderInfoAddressTo { get; set; }

    /// <summary>
    /// Цена заказа.
    /// </summary>
    public virtual OrderInfoPrice OrderInfoPrice { get; set; }

    /// <summary>
    /// Вес заказа.
    /// </summary>
    public virtual OrderInfoWeight OrderInfoWeight { get; set; }

    /// <summary>
    /// Размер заказа.
    /// </summary>
    public virtual Size Size { get; set; }
}
