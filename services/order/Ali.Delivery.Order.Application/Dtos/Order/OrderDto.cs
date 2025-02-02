namespace Ali.Delivery.Order.Application.Dtos.Order;

/// <summary>
/// Представляет заказ.
/// </summary>
/// <param name="Id">Идентификационный номер заказа.</param>
/// <param name="Name">Наименование заказа.</param>
/// <param name="OrderStatusName">Статус заказа.</param>
/// <param name="Weight">Вес заказа.</param>
/// <param name="Price">Цена заказа.</param>
/// <param name="AddressFrom">Адрес забора заказа.</param>
/// <param name="AddressTo">Адрес доставки заказа.</param>
public sealed record OrderDto(Guid Id, string Name, string OrderStatusName, decimal Weight, decimal Price, string AddressFrom, string AddressTo)
{
    /// <summary>
    /// Создает DTO объекта заказа из сущности заказа.
    /// </summary>
    /// <param name="order">Сущность заказа.</param>
    /// <returns>Экземпляр <see cref="OrderDto" />.</returns>
    public static OrderDto FromOrder(Domain.Entities.Order order) =>
        new(order.Id,
            order.Name,
            order.OrderStatus.Name,
            order.OrderInfo.OrderInfoWeight,
            order.OrderInfo.OrderInfoPrice,
            order.OrderInfo.OrderInfoAddressFrom,
            order.OrderInfo.OrderInfoAddressTo);
}
