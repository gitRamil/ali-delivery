using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.CreateOrder;

/// <summary>
/// Представляет команду создания заказа.
/// </summary>
/// <param name="OrderName">Наименование заказа.</param>
/// <param name="Weight">Информация о заказе.</param>
/// <param name="Size">Код размера заказа.</param>
/// <param name="Price">Цена заказа.</param>
/// <param name="AddressFrom">Адрес отправления.</param>
/// <param name="AddressTo">Адрес доставки.</param>
/// <param name="ReceiverId">ID получателя.</param>
public record CreateOrderCommand(string OrderName, decimal Weight, SizeCode Size, decimal Price, string AddressFrom, string AddressTo, Guid ReceiverId ) : IRequest<Guid>;
