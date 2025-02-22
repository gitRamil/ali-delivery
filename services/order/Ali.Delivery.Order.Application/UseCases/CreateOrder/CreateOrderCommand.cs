using Ali.Delivery.Order.Application.Dtos.Enums;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.CreateOrder;

/// <summary>
/// Представляет команду создания заказа.
/// </summary>
/// <param name="OrderName">Наименование.</param>
/// <param name="Weight">Вес.</param>
/// <param name="Size">Размер.</param>
/// <param name="Price">Цена.</param>
/// <param name="AddressFrom">Адрес отправления.</param>
/// <param name="AddressTo">Адрес доставки.</param>
/// <param name="ReceiverId">ID получателя.</param>
public record CreateOrderCommand(string OrderName, decimal Weight, SizeCode Size, decimal Price, string AddressFrom, string AddressTo, Guid ReceiverId) : IRequest<Guid>;
