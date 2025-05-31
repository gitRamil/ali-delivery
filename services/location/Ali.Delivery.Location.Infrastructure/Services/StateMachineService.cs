using Ali.Delivery.Location.Infrastructure.ExternalServices.Models; 
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; 
using Microsoft.Extensions.Logging;

using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Services;

public class StateMachineService(IConfiguration configuration, IMemoryCache stateCache, ILogger<StateMachineService> logger, IServiceScopeFactory serviceScopeFactory)
    : IStateMachine
{
    private const string StateCacheKey = "user_state_{0}";
    private const string LoginCacheKey = "user_login_{0}";
    
    public async Task<StateTransitionResult> ProcessUpdateAsync(long userId, Update update)
    {
        var currentState = await GetUserStateAsync(userId);
        logger.LogDebug("Processing update for user {UserId} in state {CurrentState}. Update type: {UpdateType}", userId, currentState, update.Type);
        
        using (var scope = serviceScopeFactory.CreateScope())
        {
            var scopedServiceProvider = scope.ServiceProvider;
            var handlersEnumerable = scopedServiceProvider.GetServices<ICommandHandler>();
            
            // Материализуем коллекцию один раз
            var handlersList = handlersEnumerable.ToList(); 

            if (!handlersList.Any()) // Работаем с materialized handlersList
            {
                logger.LogWarning("No command handlers registered for user {UserId} in state {CurrentState}. Update type: {UpdateType}", userId, currentState, update.Type);
                // Можно рассмотреть возврат ошибки или специального состояния, если отсутствие обработчиков критично
            }

            foreach (var handler in handlersList) // Работаем с materialized handlersList
            {
                logger.LogTrace("Trying handler {HandlerType} for user {UserId}", handler.GetType().Name, userId);
                var result = await handler.HandleAsync(userId, update, currentState);
                if (result.IsHandled)
                {
                    logger.LogDebug("Handler {HandlerType} handled update for user {UserId}. Next transition key: {NextTransitionKey}", handler.GetType().Name, userId, result.NextTransition);
                    var transition = GetTransition(currentState, result.NextTransition);
                    if (transition != null)
                    {
                        await SetUserStateAsync(userId, transition.NextState);
                        
                        if (result.UserData != null)
                        {
                            SaveUserData(userId, result.UserData);
                        }
                        
                        return new StateTransitionResult(
                            transition.NextState,
                            transition.Notification,
                            GetNotificationMessage(transition.Notification, result.UserData),
                            result.UserData
                        );
                    }
                    logger.LogWarning("No valid transition found for key {NextTransitionKey} from state {CurrentState} for user {UserId}", result.NextTransition, currentState, userId);
                }
            }
        }

        logger.LogInformation("No handler processed the update for user {UserId}. Handling as unknown action.", userId);
        return HandleUnknownAction(currentState, update);
    }

    // Метод стал синхронным, так как IMemoryCache. Set синхронный
    private void SaveUserData(long userId, Dictionary<string, object> userData)
    {
        if (userData.TryGetValue("Login", out var loginObj) && loginObj is string login) // Более безопасное получение и проверка типа
        {
            if (!string.IsNullOrEmpty(login)) // Дополнительная проверка на пустую строку
            {
                stateCache.Set(
                    string.Format(LoginCacheKey, userId),
                    login,
                    TimeSpan.FromHours(24));

                logger.LogInformation("Cached login '{Login}' for user {UserId}", login, userId);
            }
            else
            {
                logger.LogWarning("Attempted to cache empty login for user {UserId}", userId);
            }
        }
        else
        {
            logger.LogDebug("UserData for user {UserId} does not contain a valid 'Login' entry.", userId);
        }
    }

    private StateTransition? GetTransition(BotState currentState, string? transitionKey)
    {
        if (string.IsNullOrEmpty(transitionKey))
        {
            logger.LogDebug("Transition key is null or empty for state {CurrentState}.", currentState);
            return null;
        }
        
        var transitionConfigPath = $"BotConfiguration:StateTransitions:{currentState}:{transitionKey}";
        var transitionSection = configuration.GetSection(transitionConfigPath);
            
        if (!transitionSection.Exists())
        {
            logger.LogWarning("Transition configuration not found at path: {TransitionConfigPath}", transitionConfigPath);
            return null;
        }

        var nextStateValue = transitionSection["NextState"];
        var notificationValue = transitionSection["Notification"];
        
        if (string.IsNullOrEmpty(nextStateValue))
        {
            logger.LogWarning("NextState is not configured for transition at path: {TransitionConfigPath}", transitionConfigPath);
            return null;
        }

        if (!Enum.TryParse<BotState>(nextStateValue, true, out var nextState)) // Добавлено true для ignoreCase
        {
            logger.LogWarning("Invalid NextState value '{NextStateValue}' at path: {TransitionConfigPath}", nextStateValue, transitionConfigPath);
            return null;
        }

        NotificationType? notification = null;
        if (!string.IsNullOrEmpty(notificationValue))
        {
            if (!Enum.TryParse<NotificationType>(notificationValue, true, out var parsedNotification)) // Добавлено true для ignoreCase
            {
                logger.LogWarning("Invalid NotificationType value '{NotificationValue}' at path: {TransitionConfigPath}. Proceeding without notification.", notificationValue, transitionConfigPath);
            }
            else
            {
                notification = parsedNotification;
            }
        }
        
        logger.LogDebug("Transition from {CurrentState} with key {TransitionKey} to {NextState} with notification {NotificationType}", currentState, transitionKey, nextState, notification?.ToString() ?? "None");
        return new StateTransition(nextState, notification);
    }

    // Метод стал синхронным
    private StateTransitionResult HandleUnknownAction(BotState currentState, Update update)
    {
        var errorTransitionKey = currentState switch
        {
            BotState.WaitingAuthCommand => "OnInvalidCommand",
            BotState.WaitingCredentials => "OnInvalidCredentials",
            BotState.GeosharingActive when update.Message?.Location != null => "OnInvalidLocation", // Было update.Message?.Location != null
            // Рассмотрите добавление обработчика для других типов update, если нужно
            _ => null
        };

        if (errorTransitionKey != null)
        {
            logger.LogDebug("Handling unknown action with error transition key: {ErrorTransitionKey} for state {CurrentState}", errorTransitionKey, currentState);
            var transition = GetTransition(currentState, errorTransitionKey);
            if (transition != null)
            {
                return new StateTransitionResult(
                    transition.NextState,
                    transition.Notification,
                    GetNotificationMessage(transition.Notification) 
                );
            }
            logger.LogWarning("Error transition found for key {ErrorTransitionKey} but GetTransition returned null for state {CurrentState}.", errorTransitionKey, currentState);
        }

        logger.LogInformation("No specific error transition or handler for current state {CurrentState} and update type {UpdateType}. Returning current state.", currentState, update.Type);
        return new StateTransitionResult(currentState, null, null); // Сообщение не отправляется, состояние не меняется
    }

    // Добавлен параметр UserData для возможности форматирования сообщений
    private string? GetNotificationMessage(NotificationType? notification, Dictionary<string, object>? userData = null)
{
    if (notification == null) return null;

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
            "Пожалуйста, используйте доступные команды для продолжения.", 
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
    
    // Пример форматирования сообщения с использованием UserData для N6_LocationReceived
    if (notification == NotificationType.N6_LocationReceived && userData != null)
    {
        if (userData.TryGetValue("Latitude", out var latObj) && userData.TryGetValue("Longitude", out var lonObj))
        {
            // Используем форматирование для чисел с плавающей точкой, чтобы избежать слишком длинных дробей
            var latitude = Convert.ToDouble(latObj).ToString("F4"); // 4 знака после запятой
            var longitude = Convert.ToDouble(lonObj).ToString("F4"); // 4 знака после запятой
            message = $"Локация получена: Широта {latitude}, Долгота {longitude}. Продолжайте делиться или введите /stop.";
        }
    }
    
    logger.LogInformation("Generated notification message for type {NotificationType}: '{Message}'", notification, message);
    return message;
}

    public Task<BotState> GetUserStateAsync(long userId)
    {
        var cacheKey = string.Format(StateCacheKey, userId);
        // GetOrCreateAsync уже возвращает Task<BotState>
        return stateCache.GetOrCreateAsync(cacheKey, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24);
            logger.LogInformation("User {UserId} state initialized to Initial as not found in cache.", userId);
            return Task.FromResult(BotState.Initial);
        });
    }

    // Метод стал синхронным, но для совместимости с интерфейсом возвращает Task. CompletedTask
    public Task SetUserStateAsync(long userId, BotState state)
    {
        var cacheKey = string.Format(StateCacheKey, userId);
        stateCache.Set(cacheKey, state, TimeSpan.FromHours(24));
        logger.LogInformation("User {UserId} state changed to {State}", userId, state);
        return Task.CompletedTask; // Возвращаем завершенную задачу
    }
}

public record StateTransition(BotState NextState, NotificationType? Notification);
