namespace Ali.Delivery.Order.Application.Dtos.Order;

/// <summary>
/// Представляет заказ.
/// </summary>
/// <param name="Id">Идентификатор.</param>
/// <param name="Name">Наименование.</param>
/// <param name="OrderStatusName">Статус.</param>
/// <param name="Weight">Вес.</param>
/// <param name="Price">Цена.</param>
/// <param name="AddressFrom">Адрес отправления.</param>
/// <param name="AddressTo">Адрес доставки.</param>
public sealed record OrderDto(Guid Id, string Name, string OrderStatusName, decimal Weight, decimal Price, string AddressFrom, string AddressTo)
{
    /// <summary>
    /// Создает DTO объекта заказа из сущности заказа.
    /// </summary>
    /// <param name="order">Сущность заказа.</param>
    /// <returns>Экземпляр <see cref="OrderDto" />.</returns>
    public static OrderDto FromOrder(Domain.Entities.Order order) =>
        new(order.Id, order.Name, order.OrderStatus.Name, order.OrderInfo.Weight, order.OrderInfo.Price, order.OrderInfo.AddressFrom, order.OrderInfo.AddressTo);
}
