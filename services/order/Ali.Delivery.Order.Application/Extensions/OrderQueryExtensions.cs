namespace Ali.Delivery.Order.Application.Extensions;

/// <summary>
/// 
/// </summary>
public static class OrderQueryExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="query"></param>
    /// <param name="orderStatus"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public static IQueryable<Domain.Entities.Order> CheckOrderStatusForCourier(this IQueryable<Domain.Entities.Order> query, string orderStatus, Guid? userId = null)
    {
        return userId.HasValue
                   ? query.Where(o => o.Courier != null && (Guid)o.Courier.Id == userId.Value && o.OrderStatus.Code == orderStatus)
                   : query.Where(o => o.OrderStatus.Code == orderStatus);
    }
}