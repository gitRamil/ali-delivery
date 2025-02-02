using Ali.Delivery.Order.Domain.Entities.Dictionaries;

namespace Ali.Delivery.Order.Application.Extensions;

/// <summary>
/// Содержит набор методов расширения для работы с заказами.
/// </summary>
public static class OrderQueryExtensions
{
    /// <summary>
    /// Представляет метод расширения для проверки статуса заказа для сущности курьера.
    /// </summary>
    /// <param name="query">Запрос.</param>
    /// <param name="orderStatus">Статус заказа.</param>
    /// <param name="courierId">Id курьера.</param>
    /// <returns></returns>
    public static IQueryable<Domain.Entities.Order> CheckOrderStatusForCourier(this IQueryable<Domain.Entities.Order> query, OrderStatus orderStatus, Guid courierId)
    {
        return query.Where(o => o.Courier != null && (Guid)o.Courier.Id == courierId && o.OrderStatus == orderStatus);
    }
}
