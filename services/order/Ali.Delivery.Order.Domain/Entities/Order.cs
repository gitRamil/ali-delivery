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
    /// <param name="sender">Отправитель.</param>
    /// <param name="receiver">Получатель.</param>
    /// <param name="courier">Курьер.</param>
    /// <param name="notAuthReceiver">Незарегистрированный получатель.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="orderName" /> или
    /// <paramref name="orderInfo" /> или
    /// <paramref name="orderStatus" /> или
    /// <paramref name="sender" /> равен <c>null</c>.
    /// </exception>
    public Order(SequentialGuid id,
                 OrderName orderName,
                 OrderInfo orderInfo,
                 OrderStatus orderStatus,
                 User sender,
                 User? receiver,
                 NotAuthUser? notAuthReceiver,
                 User? courier)
        : base(id)
    {
        if ((receiver == null && notAuthReceiver == null) || (receiver != null && notAuthReceiver != null))
        {
            throw new InvalidOperationException("Должен быть указан либо зарегистрированный, либо незарегистрированный получатель.");
        }

        if (sender.PassportInfo == null)
        {
            throw new InvalidOperationException("Пожалуйста заполните паспортные данные для создания заказа");
        }

        Name = orderName ?? throw new ArgumentNullException(nameof(orderName));
        OrderInfo = orderInfo ?? throw new ArgumentNullException(nameof(orderInfo));
        OrderStatus = orderStatus ?? throw new ArgumentNullException(nameof(orderStatus));
        Sender = sender ?? throw new ArgumentNullException(nameof(sender));
        Receiver = receiver;
        Courier = courier;
        NotAuthReceiver = notAuthReceiver;
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
        Sender = null!;
        Receiver = null!;
        Courier = null!;
        NotAuthReceiver = null!;
    }

    /// <summary>
    /// Возвращает курьера.
    /// </summary>
    public virtual User? Courier { get; private set; }

    /// <summary>
    /// Возвращает наименование.
    /// </summary>
    public OrderName Name { get; private set; }

    /// <summary>
    /// Возвращает незарегистрированного получателя.
    /// </summary>
    public virtual NotAuthUser? NotAuthReceiver { get; private set; }

    /// <summary>
    /// Возвращает информацию заказа.
    /// </summary>
    public virtual OrderInfo OrderInfo { get; private set; }

    /// <summary>
    /// Возвращает статус заказа.
    /// </summary>
    public virtual OrderStatus OrderStatus { get; private set; }

    /// <summary>
    /// Возвращает получателя.
    /// </summary>
    public virtual User? Receiver { get; private set; }

    /// <summary>
    /// Возвращает отправителя.
    /// </summary>
    public virtual User Sender { get; private set; }

    /// <summary>
    /// Завершить заказ.
    /// </summary>
    /// <param name="currentUser">Текущий пользователь.</param>
    /// <exception cref="UnauthorizedAccessException"></exception>
    public void FinishDelivery(User currentUser)
    {
        if (Courier != null && Courier != currentUser)
        {
            throw new UnauthorizedAccessException("Текущий пользователь не является назначенным курьером для этого заказа.");
        }

        OrderStatus = OrderStatus.Finished;
    }

    /// <summary>
    /// Назначить курьера на заказ.
    /// </summary>
    /// <param name="courier">Курьер.</param>
    /// <exception cref="InvalidOperationException"></exception>
    public void SetCourier(User courier)
    {
        if (courier.PassportInfo == null)
        {
            throw new InvalidOperationException("Пожалуйста заполните паспортные данные для продолжения работы");
        }

        ArgumentNullException.ThrowIfNull(OrderStatus);

        var notAllowedOrderStatuses = new List<OrderStatus>
        {
            OrderStatus.InProgress,
            OrderStatus.Finished
        };

        if (notAllowedOrderStatuses.Contains(OrderStatus))
        {
            throw new InvalidOperationException($"Нельзя назначить курьера, если заказ находится в статусах: {string.Join(", ", notAllowedOrderStatuses.Select(s => s.Name))}");
        }

        OrderStatus = OrderStatus.InProgress;
        Courier = courier;
    }

    /// <summary>
    /// Снять курьера с заказа.
    /// </summary>
    /// <param name="currentUser">Текущий пользователь. </param>
    /// <exception cref="UnauthorizedAccessException"></exception>
    public void UnassignCourier(User currentUser)
    {
        if (Courier != null && Courier != currentUser)
        {
            throw new UnauthorizedAccessException("Текущий пользователь не является назначенным курьером для этого заказа.");
        }

        Courier = null;
        OrderStatus = OrderStatus.Created;
    }

    /// <summary>
    /// Обновляет наименование заказа.
    /// </summary>
    /// <param name="name">Наименование.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="name" /> равен <c>null</c>.
    /// </exception>
    public void UpdateOrderName(OrderName name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    /// <summary>
    /// Обновляет статус заказа.
    /// </summary>
    /// <param name="orderStatus">Статус заказа.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="orderStatus" /> равен <c>null</c>.
    /// </exception>
    public void UpdateOrderStatus(OrderStatus orderStatus)
    {
        OrderStatus = orderStatus ?? throw new ArgumentNullException(nameof(orderStatus));
    }
}
