using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.Order;

namespace Ali.Delivery.Order.Domain.Entities;

/// <summary>
/// Представляет сущность заказа.
/// </summary>
public class Order : Entity<SequentialGuid>
{
    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="Order" />.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="name">Наименование цели.</param>
    /// <param name="orderInfo"></param>
    /// <param name="orderStatus">Наименование цели.</param>
    /// &lt;param name="name"&gt;Наименование цели.&lt;/param&gt;
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="name" /> или
    /// <paramref name="name" /> равен <c>null</c>.
    /// </exception>
    public Order(SequentialGuid id, OrderName name, OrderInfo orderInfo, OrderStatus orderStatus)
        : base(id)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        OrderInfo = orderInfo ?? throw new ArgumentNullException(nameof(orderInfo));
        OrderStatus = orderStatus ?? throw new ArgumentNullException(nameof(orderStatus));
    }

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="Order" />.
    /// </summary>
    /// <remarks>Конструктор для EF.</remarks>
    protected Order()
        : base(SequentialGuid.Empty)
    {
        Name = null!;
        OrderStatus = null!;
        OrderInfo = null!;
    }

    /// <summary>
    /// Возвращает наименование заказа.
    /// </summary>
    public OrderName Name { get; }

    // /// <summary>
    // /// Возвращает информацию заказа.
    // /// </summary>
    public virtual OrderInfo OrderInfo { get; }

    /// <summary>
    /// Возвращает статус заказа.
    /// </summary>
    public virtual OrderStatus OrderStatus { get; }
}
