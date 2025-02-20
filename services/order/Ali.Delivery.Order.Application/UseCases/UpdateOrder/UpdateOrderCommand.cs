using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Application.Dtos.Order.Enum;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.UpdateOrder;

/// <summary>
/// Представляет команду обновления заказа.
/// </summary>
/// <param name="OrderId">Идентификатор.</param>
/// <param name="OrderName">Наименование.</param>
/// <param name="Weight">Вес.</param>
/// <param name="Size">Размер.</param>
/// <param name="Price">Цена.</param>
/// <param name="AddressFrom">Адрес отправления.</param>
/// <param name="AddressTo">Адрес доставки.</param>
/// <param name="OrderStatus">Статус.</param>
public record UpdateOrderCommand(Guid OrderId, string OrderName, decimal Weight, SizeCode Size, decimal Price, string AddressFrom, string AddressTo, OrderStatus OrderStatus)
    : IRequest<OrderDto>;
