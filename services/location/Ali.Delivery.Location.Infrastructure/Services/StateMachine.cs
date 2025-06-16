using Ali.Delivery.Location.Infrastructure.ExternalServices.Models.Configuration;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Services;

public class StateMachine : IStateMachine // TODO: Проверить нужно ли еще что-то вытащить в отдельный класс.
{
    private readonly StateMachineConfiguration _config;
    private readonly IStepHandlerMapping _stepHandlerMapping;
    private readonly IUserStateService _userStateService; 
    private readonly ITelegramBotClient _botClient;
    private readonly INotificationService _notification;

    public StateMachine(
        IOptions<StateMachineConfiguration> config, 
        IStepHandlerMapping stepHandlerMapping,
        IUserStateService userStateService,
        ITelegramBotClient botClient,
        INotificationService notification)
    {
        _config = config.Value;
        _stepHandlerMapping = stepHandlerMapping;
        _userStateService = userStateService;
        _botClient = botClient;
        _notification = notification;
    }

    public async Task ProcessUpdateAsync(Update update)
    {
        // Нас интересуют только сообщения с текстом
        if (update.Message is null || update.Message.From is null)
        {
            return;
        }

        var userId = update.Message.From.Id;
        var chatId = update.Message.Chat.Id;
        var text   = update.Message.Text; 
        
        if (string.Equals(text, "/stop", StringComparison.OrdinalIgnoreCase))
        {
            await _userStateService.SetUserStateAsync(userId, "StopStep");
            await _stepHandlerMapping.GetHandler("StopStep").OnEnterAsync(chatId);
            return;
        }

        // 1. Получаем текущее состояние пользователя
        var currentStateId = await _userStateService.GetUserStateAsync(userId);
        
        // 2. Получаем обработчик для этого состояния
        var handler = _stepHandlerMapping.GetHandler(currentStateId);
        
        // 3. Передаем управление обработчику
        var result = await handler.HandleAsync(update);

        // 4. Определяем следующий шаг по результату от обработчика
        var currentStepConfig = GetStep(currentStateId);
        
        // Важный момент: в вашей JSON-схеме команда - это ключ. 
        // Поэтому ищем следующий шаг по ключу.
        if (string.IsNullOrWhiteSpace(result.NextStepOption))
        {
            return;
        }

        // 2. стандартный поиск перехода
        if (!currentStepConfig.SelectNextStep.TryGetValue(result.NextStepOption, out var nextStateId))
        {
            await _botClient.SendMessage(chatId,
                                         _notification.GenerateNotificationMessage(NotificationType.N1_InvalidCommand)!);
            return;
        }

        // 5. Обновляем состояние пользователя
        await _userStateService.SetUserStateAsync(userId, nextStateId);
        
        // 6. (ОЧЕНЬ ВАЖНО) Вызываем "вход" в новое состояние, чтобы отправить приветственное сообщение
        var nextHandler = _stepHandlerMapping.GetHandler(nextStateId);
        await nextHandler.OnEnterAsync(chatId);
    }

    private StepConfiguration GetStep(string stepId) =>
        _config.Steps.FirstOrDefault(s => s.Id == stepId) 
        ?? throw new InvalidOperationException($"Конфигурация для шага '{stepId}' не найдена.");
}