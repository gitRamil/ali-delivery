namespace Ali.Delivery.Order.Application.Dtos.Order;

/// <summary>
/// Представляет набор значений, описывающих размеры посылок.
/// </summary>
/// <param name="Code">Код.</param>
/// <param name="Name">Наименование.</param>
public sealed record SizeDto(string Code, string Name);
