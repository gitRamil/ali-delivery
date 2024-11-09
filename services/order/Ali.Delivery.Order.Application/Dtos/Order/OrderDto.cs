namespace Ali.Delivery.Order.Application.Dtos.Order;

/// <summary>
/// Представляет заказ.
/// </summary>
/// <param name="Id">Идентификационный номер заказа.</param>
/// <param name="Name">Наименование заказа.</param>
public sealed record OrderDto(Guid Id, string Name);
