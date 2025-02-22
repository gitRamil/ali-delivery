namespace Ali.Delivery.Order.Application.Dtos.Order;

/// <summary>
/// Представляет значение справочника.
/// </summary>
/// <param name="Code">Код значения.</param>
/// <param name="Name">Название значения.</param>
public sealed record DictionaryDto(string Code, string Name);
