using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection; 
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options; 
using System.Globalization; 
using Ali.Delivery.Location.Infrastructure.ExternalServices.Models.Configuration;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Services;

public class StateMachineService : IStateMachine
{
    private const string StateCacheKey = "user_state_{0}";
    private const string LoginCacheKey = "user_login_{0}";
    
    private readonly StateTransitionsConfig _transitionsConfig;
    private readonly IMemoryCache _stateCache;
    private readonly ILogger<StateMachineService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public StateMachineService(
        IOptions<StateTransitionsConfig> transitionsConfigOptions,
        IMemoryCache stateCache,
        ILogger<StateMachineService> logger,
        IServiceScopeFactory serviceScopeFactory)
    {
        _transitionsConfig = transitionsConfigOptions.Value ?? throw new ArgumentNullException(nameof(transitionsConfigOptions), "State transitions configuration cannot be null.");
        _stateCache = stateCache ?? throw new ArgumentNullException(nameof(stateCache));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
    }
    
    public async Task<StateTransitionResult> ProcessUpdateAsync(long userId, Update update)
    {
        var currentState = await GetUserStateAsync(userId);
        _logger.LogDebug("Processing update for user {UserId} in state {CurrentState}. Update type: {UpdateType}", userId, currentState, update.Type);
        
        // Создаем новую область видимости для обработки этого обновления
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var scopedServiceProvider = scope.ServiceProvider;
            // Материализуем коллекцию обработчиков один раз
            var handlersList = scopedServiceProvider.GetServices<ICommandHandler>().ToList(); 

            if (!handlersList.Any())
            {
                _logger.LogWarning("No command handlers registered. Update for user {UserId} in state {CurrentState} (type {UpdateType}) will not be processed by any specific handler.", userId, currentState, update.Type);
            }

            foreach (var handler in handlersList)
            {
                _logger.LogTrace("Trying handler {HandlerType} for user {UserId} in state {CurrentState}", handler.GetType().Name, userId, currentState);
                var result = await handler.HandleAsync(userId, update, currentState);
                if (result.IsHandled)
                {
                    _logger.LogDebug("Handler {HandlerType} handled update for user {UserId}. Next transition key: '{NextTransitionKey}'", handler.GetType().Name, userId, result.NextTransition);
                    var transition = GetTransition(currentState, result.NextTransition);
                    if (transition != null)
                    {
                        await SetUserStateAsync(userId, transition.NextState);
                        
                        if (result.UserData != null)
                        {
                            SaveUserData(userId, result.UserData); // Синхронный метод
                        }
                        
                        return new StateTransitionResult(
                            transition.NextState,
                            transition.Notification,
                            GetNotificationMessage(transition.Notification, result.UserData),
                            result.UserData
                        );
                    }
                    _logger.LogWarning("No valid transition found for key '{NextTransitionKey}' from state {CurrentState} for user {UserId}. Handler: {HandlerType}", result.NextTransition, currentState, userId, handler.GetType().Name);
                }
            }
        }

        _logger.LogInformation("No handler processed the update for user {UserId} in state {CurrentState}. Handling as unknown action.", userId, currentState);
        return HandleUnknownAction(currentState, update); // Синхронный метод
    }

    private void SaveUserData(long userId, Dictionary<string, object> userData)
    {
        if (userData.TryGetValue("Login", out var loginObj) && loginObj is string login)
        {
            if (!string.IsNullOrEmpty(login))
            {
                _stateCache.Set(
                    string.Format(LoginCacheKey, userId),
                    login,
                    TimeSpan.FromHours(24));

                _logger.LogInformation("Cached login '{Login}' for user {UserId}", login, userId);
            }
            else
            {
                _logger.LogWarning("Attempted to cache empty login for user {UserId}", userId);
            }
        }
        else
        {
            _logger.LogTrace("UserData for user {UserId} does not contain a 'Login' entry or it's not a string.", userId);
        }
    }

    private StateTransition? GetTransition(BotState currentState, string? transitionKey)
    {
        if (string.IsNullOrEmpty(transitionKey))
        {
            _logger.LogDebug("Transition key is null or empty for state {CurrentState}.", currentState);
            return null;
        }
        
        if (_transitionsConfig.TryGetValue(currentState, out var stateSpecificTransitions) &&
            stateSpecificTransitions.TryGetValue(transitionKey, out var transitionDetails))
        {
            _logger.LogDebug("Transition found for {CurrentState} with key '{TransitionKey}' -> NextState: {NextState}, Notification: {NotificationType}", 
                currentState, transitionKey, transitionDetails.NextState, transitionDetails.Notification?.ToString() ?? "None");
            return new StateTransition(transitionDetails.NextState, transitionDetails.Notification);
        }
        
        _logger.LogWarning("Transition configuration not found for CurrentState: {CurrentState}, TransitionKey: '{TransitionKey}'", currentState, transitionKey);
        return null;
    }

    private StateTransitionResult HandleUnknownAction(BotState currentState, Update update)
    {
        var errorTransitionKey = currentState switch
        {
            BotState.WaitingAuthCommand => "OnInvalidCommand",
            BotState.WaitingCredentials => "OnInvalidCommand", // Может быть, другое сообщение? Или OnInvalidCredentials если формат не тот
            BotState.Authenticated => "OnInvalidCommand",    // Обработка неизвестных команд в Authenticated
            BotState.GeosharingActive when update.Message?.Location != null => "OnInvalidLocation", // Это уже обработано бы LocationHandler, но на всякий случай
            BotState.GeosharingActive => "OnInvalidCommand", // Неизвестная команда в GeosharingActive
            // Для Terminated и Initial обычно нет "неизвестных" команд, так как они ждут /start
            _ => null
        };

        if (errorTransitionKey != null)
        {
            _logger.LogDebug("Handling unknown action with error transition key: '{ErrorTransitionKey}' for state {CurrentState} and update type {UpdateType}", errorTransitionKey, currentState, update.Type);
            var transition = GetTransition(currentState, errorTransitionKey);
            if (transition != null)
            {
                return new StateTransitionResult(
                    transition.NextState,
                    transition.Notification,
                    GetNotificationMessage(transition.Notification)
                );
            }
            _logger.LogWarning("Error transition was identified by key '{ErrorTransitionKey}' for state {CurrentState}, but GetTransition returned null (config missing?).", errorTransitionKey, currentState);
        }

        _logger.LogInformation("No specific error transition or handler for current state {CurrentState} and update type {UpdateType}. Returning current state without action.", currentState, update.Type);
        return new StateTransitionResult(currentState, null, null);
    }

    private string? GetNotificationMessage(NotificationType? notification, Dictionary<string, object>? userData = null)
    {
        if (notification == null)
        {
            return null;
        }

        var message = notification switch
        {
            NotificationType.N0_EnterCredentials => 
                "Введите логин и пароль (например, user123 pass).",
            NotificationType.N1_Welcome => 
                "Добро пожаловать! Для начала работы введите /login.",
            NotificationType.N1_InvalidAuthCommand => 
                "Неизвестная команда. Пожалуйста, введите /login для аутентификации.",
            NotificationType.N_SessionEnded => 
                "Сессия завершена. Введите /start для новой сессии.",
            NotificationType.N1_InvalidCommand => 
                "Неизвестная команда или действие для текущего состояния. Пожалуйста, используйте доступные команды.",
            NotificationType.N2_InvalidCredentials => 
                "Неверные учетные данные. Пожалуйста, введите логин и пароль повторно.",
            NotificationType.N3_RegistrationRequired => 
                "Требуется регистрация. Пожалуйста, зарегистрируйтесь или обратитесь к администратору.",
            NotificationType.N4_AuthenticationComplete => 
                "Авторизация успешно завершена. Используйте /geosharing для начала отслеживания или /stop для выхода.",
            NotificationType.N5_RequestLocation => 
                "Пожалуйста, поделитесь вашей геопозицией для продолжения или введите /stop для отмены.",
            NotificationType.N6_LocationReceived => 
                "Ваша локация получена. Продолжайте делиться или введите /stop.",
            NotificationType.N7_InvalidLocation => 
                "Не удалось сохранить локацию. Пожалуйста, попробуйте снова или введите /stop.",
            _ => $"Неизвестный тип уведомления ({notification}). Обратитесь к разработчику."
        };
        
        if (notification == NotificationType.N6_LocationReceived && userData != null)
        {
            if (userData.TryGetValue("Latitude", out var latObj) && userData.TryGetValue("Longitude", out var lonObj))
            {
                try
                {
                    // Используем CultureInfo.InvariantCulture для корректного преобразования и форматирования чисел с плавающей точкой
                    var latitude = Convert.ToDouble(latObj, CultureInfo.InvariantCulture).ToString("F4", CultureInfo.InvariantCulture);
                    var longitude = Convert.ToDouble(lonObj, CultureInfo.InvariantCulture).ToString("F4", CultureInfo.InvariantCulture);
                    message = $"Локация получена: Широта {latitude}, Долгота {longitude}. Продолжайте делиться или введите /stop.";
                }
                catch (FormatException ex)
                {
                     _logger.LogError(ex, "Error formatting latitude/longitude for notification. Raw Latitude: '{LatObj}', Raw Longitude: '{LonObj}'", latObj, lonObj);
                    // Оставить стандартное сообщение, если форматирование не удалось
                }
            }
        }
        
        _logger.LogInformation("Generated notification message for type {NotificationType}: '{Message}'", notification, message);
        return message;
    }

    public Task<BotState> GetUserStateAsync(long userId)
    {
        var cacheKey = string.Format(StateCacheKey, userId);
        return _stateCache.GetOrCreateAsync(cacheKey, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24);
            _logger.LogInformation("User {UserId} state initialized to Initial as not found in cache.", userId);
            return Task.FromResult(BotState.Initial);
        });
    }

    public Task SetUserStateAsync(long userId, BotState state)
    {
        var cacheKey = string.Format(StateCacheKey, userId);
        _stateCache.Set(cacheKey, state, TimeSpan.FromHours(24));
        _logger.LogInformation("User {UserId} state changed to {State}", userId, state);
        return Task.CompletedTask;
    }
}

// Напоминание: этот record лучше разместить в файле с моделями или интерфейсами,
// если он используется за пределами StateMachineService.cs.
// Если он используется только здесь, то его текущее расположение допустимо.
// public record StateTransition(BotState NextState, NotificationType? Notification);
// (Предполагается, что он определен в Ali.Delivery.Location.Infrastructure.Interfaces или аналогичном месте)
