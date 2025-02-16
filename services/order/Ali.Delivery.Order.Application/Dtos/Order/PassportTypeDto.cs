namespace Ali.Delivery.Order.Application.Dtos.Order;

/// <summary>
/// Представляет набор значений, описывающих типы паспорта.
/// </summary>
/// <param name="Code">Код.</param>
/// <param name="Name">Наименование.</param>
public sealed record PassportTypeDto(string Code, string Name);
