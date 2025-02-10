using MediatR;

namespace Ali.Delivery.Order.Application.UseCases.GetIsUserExist;

/// <summary>
/// Представляет запрос проверки существования пользователя.
/// </summary>
/// <param name="UserId">Идентификатор пользователя.</param>
public sealed record GetIsUserExistCommand(Guid UserId) : IRequest<bool>;
