using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Services;

public class StateMachineOrchestrator : IStateMachine
{
    private readonly IStateManager _stateManager;
    private readonly IUserDataManager _userDataManager;
    private readonly ITransitionResolver _transitionResolver;
    private readonly INotificationService _notificationService;
    private readonly IUpdateProcessor _updateProcessor;
    private readonly ILogger<StateMachineOrchestrator> _logger;

    public StateMachineOrchestrator(
        IStateManager stateManager,
        IUserDataManager userDataManager,
        ITransitionResolver transitionResolver,
        INotificationService notificationService,
        IUpdateProcessor updateProcessor,
        ILogger<StateMachineOrchestrator> logger)
    {
        _stateManager = stateManager ?? throw new ArgumentNullException(nameof(stateManager));
        _userDataManager = userDataManager ?? throw new ArgumentNullException(nameof(userDataManager));
        _transitionResolver = transitionResolver ?? throw new ArgumentNullException(nameof(transitionResolver));
        _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
        _updateProcessor = updateProcessor ?? throw new ArgumentNullException(nameof(updateProcessor));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<StateTransitionResult> ProcessUpdateAsync(long userId, Update update)
    {
        var currentState = await _stateManager.GetUserStateAsync(userId);
        _logger.LogDebug("Processing update for user {UserId} in state {CurrentState}. Update type: {UpdateType}", 
            userId, currentState, update.Type);

        var commandResult = await _updateProcessor.ProcessUpdateWithHandlersAsync(userId, update, currentState);
        
        if (commandResult.IsHandled)
        {
            return await ProcessSuccessfulCommand(userId, currentState, commandResult);
        }

        _logger.LogInformation("No handler processed the update for user {UserId} in state {CurrentState}. Handling as unknown action", 
            userId, currentState);
        
        var unknownActionResult = _transitionResolver.HandleUnknownAction(currentState, update);
        var notificationMessage = _notificationService.GenerateNotificationMessage(unknownActionResult.Notification);
        
        return new StateTransitionResult(
            unknownActionResult.NewState,
            unknownActionResult.Notification,
            notificationMessage ?? unknownActionResult.ResponseMessage,
            unknownActionResult.UserData
        );
    }

    private async Task<StateTransitionResult> ProcessSuccessfulCommand(long userId, BotState currentState, CommandResult commandResult)
    {
        var transition = _transitionResolver.ResolveTransition(currentState, commandResult.NextTransition);
        
        if (transition != null)
        {
            await _stateManager.SetUserStateAsync(userId, transition.NextState);
            
            if (commandResult.UserData != null)
            {
                _userDataManager.SaveUserData(userId, commandResult.UserData);
            }
            
            var notificationMessage = _notificationService.GenerateNotificationMessage(transition.Notification, commandResult.UserData);
            
            return new StateTransitionResult(
                transition.NextState,
                transition.Notification,
                notificationMessage,
                commandResult.UserData
            );
        }
        
        _logger.LogWarning("No valid transition found for key '{NextTransitionKey}' from state {CurrentState} for user {UserId}", 
            commandResult.NextTransition, currentState, userId);
        
        return new StateTransitionResult(currentState, null, null, commandResult.UserData);
    }

    public Task<BotState> GetUserStateAsync(long userId) => _stateManager.GetUserStateAsync(userId);
    public Task SetUserStateAsync(long userId, BotState state) => _stateManager.SetUserStateAsync(userId, state);
}
