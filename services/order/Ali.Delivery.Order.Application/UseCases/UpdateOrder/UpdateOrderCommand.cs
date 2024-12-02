using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.UpdateOrder;

/// <summary>
/// Представляет команду обновления заказа.
/// </summary>
/// <param name="OrderId">Идентификационный номер заказа.</param>
/// <param name="OrderName">Наименование заказа.</param>
/// <param name="Weight">Вес заказа.</param>
/// <param name="Size">Код размера заказа.</param>
/// <param name="Price">Цена заказа.</param>
/// <param name="AddressFrom">Адрес отправления.</param>
/// <param name="AddressTo">Адрес доставки.</param>
/// <param name="OrderStatus">Статус заказа.</param>
public record UpdateOrderCommand(Guid OrderId, string OrderName, decimal Weight, SizeCode Size, decimal Price, string AddressFrom, string AddressTo, OrderStatus OrderStatus)
    : IRequest<OrderDto>;
