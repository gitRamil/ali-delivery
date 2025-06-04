using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Services;

// Ali.Delivery.Location.Infrastructure.Services/UpdateProcessor.cs
public class UpdateProcessor : IUpdateProcessor
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<UpdateProcessor> _logger;

    public UpdateProcessor(
        IServiceScopeFactory serviceScopeFactory,
        ILogger<UpdateProcessor> logger)
    {
        _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<CommandResult> ProcessUpdateWithHandlersAsync(long userId, Update update, BotState currentState)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var scopedServiceProvider = scope.ServiceProvider;
        var handlersList = scopedServiceProvider.GetServices<ICommandHandler>().ToList();

        if (!handlersList.Any())
        {
            _logger.LogWarning("No command handlers registered. Update for user {UserId} in state {CurrentState} (type {UpdateType}) will not be processed by any specific handler", 
                userId, currentState, update.Type);
            return new CommandResult(false, null, null);
        }

        foreach (var handler in handlersList)
        {
            _logger.LogTrace("Trying handler {HandlerType} for user {UserId} in state {CurrentState}", 
                handler.GetType().Name, userId, currentState);
            
            var result = await handler.HandleAsync(userId, update, currentState);
            if (result.IsHandled)
            {
                _logger.LogDebug("Handler {HandlerType} handled update for user {UserId}. Next transition key: '{NextTransitionKey}'", 
                    handler.GetType().Name, userId, result.NextTransition);
                return result;
            }
        }

        _logger.LogInformation("No handler processed the update for user {UserId} in state {CurrentState}", userId, currentState);
        return new CommandResult(false, null, null);
    }
}
