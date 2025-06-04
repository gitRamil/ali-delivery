using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;
using Ali.Delivery.Location.Infrastructure.ExternalServices.Models.Configuration;
using Ali.Delivery.Location.Infrastructure.Interfaces2._0;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Services2._0;

public class TransitionResolver(IOptions<StateTransitionsConfig> transitionsConfigOptions, ILogger<TransitionResolver> logger) : ITransitionResolver
{
    private readonly StateTransitionsConfig _transitionsConfig = transitionsConfigOptions.Value ?? 
                                                                 throw new ArgumentNullException(nameof(transitionsConfigOptions), "State transitions configuration cannot be null");
    private readonly ILogger<TransitionResolver> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public StateTransition? ResolveTransition(BotState currentState, string? transitionKey)
    {
        if (string.IsNullOrEmpty(transitionKey))
        {
            _logger.LogDebug("Transition key is null or empty for state {CurrentState}", currentState);
            return null;
        }
        
        // Используем новый метод GetTransition
        var transitionConfig = _transitionsConfig.GetTransition(currentState, transitionKey);
        
        if (transitionConfig != null)
        {
            _logger.LogDebug("Transition found for {CurrentState} with key '{TransitionKey}' -> NextState: {NextState}, Notification: {NotificationType}", 
                currentState, transitionKey, transitionConfig.NextState, transitionConfig.Notification?.ToString() ?? "None");
            return new StateTransition(transitionConfig.NextState, transitionConfig.Notification);
        }
        
        _logger.LogWarning("Transition configuration not found for CurrentState: {CurrentState}, TransitionKey: '{TransitionKey}'", currentState, transitionKey);
        return null;
    }

    public StateTransitionResult HandleUnknownAction(BotState currentState, Update update)
    {
        var errorTransitionKey = DetermineErrorTransitionKey(currentState, update);

        if (errorTransitionKey != null)
        {
            _logger.LogDebug("Handling unknown action with error transition key: '{ErrorTransitionKey}' for state {CurrentState} and update type {UpdateType}", 
                errorTransitionKey, currentState, update.Type);
            
            var transitionConfig = _transitionsConfig.GetTransition(currentState, errorTransitionKey);
            if (transitionConfig != null)
            {
                return new StateTransitionResult(
                    transitionConfig.NextState, 
                    transitionConfig.Notification, 
                    null);
            }
            
            _logger.LogWarning("Error transition was identified by key '{ErrorTransitionKey}' for state {CurrentState}, but no configuration found", 
                errorTransitionKey, currentState);
        }

        _logger.LogInformation("No specific error transition or handler for current state {CurrentState} and update type {UpdateType}. Returning current state without action", 
            currentState, update.Type);
        return new StateTransitionResult(currentState, null, null);
    }

    private string? DetermineErrorTransitionKey(BotState currentState, Update update)
    {
        return currentState switch
        {
            BotState.WaitingAuthCommand => "OnInvalidCommand",
            BotState.WaitingCredentials => "OnInvalidCredentials", // Изменил для соответствия JSON
            BotState.Authenticated => "OnInvalidCommand",
            BotState.GeosharingActive when update.Message?.Location != null => "OnInvalidLocation",
            BotState.GeosharingActive => "OnInvalidCommand",
            _ => null
        };
    }
}
