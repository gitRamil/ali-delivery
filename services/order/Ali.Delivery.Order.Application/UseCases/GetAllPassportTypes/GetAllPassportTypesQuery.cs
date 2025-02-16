using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.GetAllPassportTypes;

/// <summary>
/// Представляет команду получения всех типов паспорта.
/// </summary>
public record GetAllPassportTypesQuery : IRequest<List<PassportTypeDto>>;
