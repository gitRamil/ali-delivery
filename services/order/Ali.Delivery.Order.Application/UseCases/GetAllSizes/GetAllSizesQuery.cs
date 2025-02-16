using Ali.Delivery.Order.Application.Dtos.Order;
using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.GetAllSizes;

/// <summary>
/// Представляет команду получения всех размеров посылки.
/// </summary>
public record GetAllSizesQuery : IRequest<List<SizeDto>>;
