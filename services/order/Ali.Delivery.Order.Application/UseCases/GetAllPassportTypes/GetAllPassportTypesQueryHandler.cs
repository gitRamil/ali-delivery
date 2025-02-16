using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Application.UseCases.GetAllRoles;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.GetAllPassportTypes;

/// <summary>
/// Представляет обработчик команды получения всех типова паспорта.
/// </summary>
public class GetAllPassportTypesQueryHandler : IRequestHandler<GetAllPassportTypesQuery, List<PassportTypeDto>>
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="GetAllRolesQueryHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    public GetAllPassportTypesQueryHandler(IAppDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <inheritdoc />
    public async Task<List<PassportTypeDto>> Handle(GetAllPassportTypesQuery query, CancellationToken cancellationToken)
    {
        return await _context.Types.Select(r => new PassportTypeDto(r.Code, r.Name))
                             .ToListAsync(cancellationToken);
    }
}
