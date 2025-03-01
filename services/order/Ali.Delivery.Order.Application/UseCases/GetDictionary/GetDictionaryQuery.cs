using Ali.Delivery.Order.Application.Dtos.Enums;
using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.GetDictionary;

/// <summary>
/// Представляет запрос получения списка значений справочника.
/// </summary>
/// <param name="DictionaryCode">Код справочника.</param>
public sealed record GetDictionaryQuery(DictionaryCode DictionaryCode) : IRequest<IReadOnlyCollection<DictionaryDto>>;
