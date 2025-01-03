﻿using Ali.Delivery.Domain.Core;
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
    /// <param name="orderName">Наименование заказа.</param>
    /// <param name="orderInfo">Информация о заказе.</param>
    /// <param name="orderStatus">Статус заказа.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если любой из параметров <paramref name="orderName" />,
    /// <paramref name="orderInfo" />, <paramref name="orderStatus" /> равен <c>null</c>.
    /// </exception>
    public Order(SequentialGuid id, OrderName orderName, OrderInfo orderInfo, OrderStatus orderStatus)
        : base(id)
    {
        Name = orderName ?? throw new ArgumentNullException(nameof(orderName));
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
    public OrderName Name { get; private set; }

    /// <summary>
    /// Возвращает информацию заказа.
    /// </summary>
    public virtual OrderInfo OrderInfo { get; private set; }

    /// <summary>
    /// Возвращает статус заказа.
    /// </summary>
    public virtual OrderStatus OrderStatus { get; private set; }

    /// <summary>
    /// Возвращает информацию о пользователе.
    /// </summary>
    public virtual User? Sender { get; }

    /// <summary>
    /// Обновляет наименование заказа.
    /// </summary>
    /// <param name="name"></param>
    public void UpdateOrderName(OrderName name)
    {
        ArgumentNullException.ThrowIfNull(name);
        Name = name;
    }

    /// <summary>
    /// Обновляет статус заказа.
    /// </summary>
    /// <param name="orderStatus"></param>
    public void UpdateOrderStatus(OrderStatus orderStatus)
    {
        ArgumentNullException.ThrowIfNull(orderStatus);
        OrderStatus = orderStatus;
    }
}
