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
    /// <param name="orderName">Наименование заказа.</param>
    /// <param name="orderInfo">Информация о заказе.</param>
    /// <param name="orderStatus">Статус заказа.</param>
    /// <param name="sender">Отправитель.</param>
    /// <param name="receiver">Получатель.</param>
    /// <param name="courier">Курьер.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если любой из параметров <paramref name="orderName" />,
    /// <paramref name="orderInfo" />, <paramref name="orderStatus" /> равен <c>null</c>.
    /// </exception>
    public Order(SequentialGuid id, OrderName orderName, OrderInfo orderInfo, OrderStatus orderStatus, User sender, User receiver, User? courier = null) : base(id)
    {
        Name = orderName ?? throw new ArgumentNullException(nameof(orderName));
        OrderInfo = orderInfo ?? throw new ArgumentNullException(nameof(orderInfo));
        OrderStatus = orderStatus ?? throw new ArgumentNullException(nameof(orderStatus));
        Sender = sender ?? throw new ArgumentNullException(nameof(sender));
        Receiver = receiver ?? throw new ArgumentNullException(nameof(receiver));
        Courier = courier ;
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
    public virtual OrderStatus OrderStatus { get; set; }

    
    /// <summary>
    /// Возвращает отправителя.
    /// </summary>
    public virtual User? Sender { get; }
    
    /// <summary>
    /// Возвращает получателя.
    /// </summary>
    public virtual User? Receiver { get; }
    
    /// <summary>
    /// Возвращает курьера.
    /// </summary>
    public virtual User? Courier { get; set; }

    /// <summary>
    /// Обновляет наименование заказа.
    /// </summary>
    /// <param name="name"></param>
    public void UpdateOrderName(OrderName name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    /// <summary>
    /// Обновляет статус заказа.
    /// </summary>
    /// <param name="orderStatus"></param>
    public void UpdateOrderStatus(OrderStatus orderStatus)
    {
        OrderStatus = orderStatus ?? throw new ArgumentNullException(nameof(orderStatus));
    }
}