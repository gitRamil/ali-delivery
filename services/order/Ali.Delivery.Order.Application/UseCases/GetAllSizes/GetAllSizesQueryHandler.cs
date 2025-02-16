using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Application.UseCases.GetAllRoles;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.GetAllSizes;

/// <summary>
/// Представляет обработчик команды получения всех размеров посылки.
/// </summary>
public class GetAllSizesQueryHandler : IRequestHandler<GetAllSizesQuery, List<SizeDto>>
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="GetAllRolesQueryHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    public GetAllSizesQueryHandler(IAppDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <inheritdoc />
    public async Task<List<SizeDto>> Handle(GetAllSizesQuery query, CancellationToken cancellationToken)
    {
        return await _context.Sizes.Select(r => new SizeDto(r.Code, r.Name))
                             .ToListAsync(cancellationToken);
    }
}
