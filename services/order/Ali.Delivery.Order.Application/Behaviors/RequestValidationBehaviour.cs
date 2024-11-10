using Ali.Delivery.Order.Application.Abstractions;
using MediatR;

namespace Ali.Delivery.Order.Application.Behaviors;

/// <summary>
/// Реализует сквозную функциональность по проверке достоверности <see cref="IRequest" />.
/// </summary>
/// <typeparam name="TRequest">Тип запроса.</typeparam>
/// <typeparam name="TResponse">Тип ответа.</typeparam>
public class RequestValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseRequest
{
    private readonly IValidationService _validationService;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="RequestValidationBehaviour{TRequest,TResponse}" />.
    /// </summary>
    /// <param name="validationService">Служба валидации.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="validationService" /> равен <c>null</c>.
    /// </exception>
    public RequestValidationBehaviour(IValidationService validationService) => _validationService = validationService ?? throw new ArgumentNullException(nameof(validationService));

    /// <inheritdoc />
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        await _validationService.ValidateAsync(request, cancellationToken);
        return await next();
    }
}
