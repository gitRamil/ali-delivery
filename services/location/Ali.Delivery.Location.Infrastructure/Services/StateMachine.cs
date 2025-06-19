using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;
using Ali.Delivery.Location.Infrastructure.ExternalServices.Models.Configuration;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Services;

public class StateMachine : IStateMachine // TODO: Проверить нужно ли еще что-то вытащить в отдельный класс.
{
    private readonly ITelegramBotClient _botClient;
    private readonly StateMachineConfiguration _config;
    private readonly INotificationService _notification;
    private readonly IStepHandlerMapping _stepHandlerMapping;
    private readonly IUserStateService _userStateService;

    public StateMachine(IOptions<StateMachineConfiguration> config,
                        IStepHandlerMapping stepHandlerMapping,
                        IUserStateService userStateService,
                        ITelegramBotClient botClient,
                        INotificationService notification)
    {
        _config = config.Value ?? throw new ArgumentNullException(nameof(config));
        _stepHandlerMapping = stepHandlerMapping ?? throw new ArgumentNullException(nameof(stepHandlerMapping));
        _userStateService = userStateService ?? throw new ArgumentNullException(nameof(userStateService));
        _botClient = botClient ?? throw new ArgumentNullException(nameof(botClient));
        _notification = notification ?? throw new ArgumentNullException(nameof(notification));
    }

    public async Task ProcessUpdateAsync(Update update)
    {
        if (update.Message?.From is null)
        {
            return;
        }

        var userId = update.Message.From.Id;
        var chatId = update.Message.Chat.Id;
        var text = update.Message.Text;

        if (string.Equals(text, "/stop", StringComparison.OrdinalIgnoreCase))
        {
            await _userStateService.SetUserStepAsync(userId, "StopStep");
            return;
        }

        var currentUserStepId = await _userStateService.GetUserStepIdAsync(userId);
        var currentStepConfig = GetStepConfigById(currentUserStepId);
        var handler = _stepHandlerMapping.GetHandler(currentUserStepId);
        var result = await handler.HandleAsync(UpdateConvertToMessageInfo(update));

        if (string.IsNullOrWhiteSpace(result.NextStepOption))
        {
            return;
        }

        if (!currentStepConfig.SelectNextStep.TryGetValue(result.NextStepOption, out var nextStepId))
        {
            await _botClient.SendMessage(chatId, _notification.GenerateNotificationMessage(NotificationType.N1_InvalidCommand));
            return;
        }

        await _userStateService.SetUserStepAsync(userId, nextStepId);
    }

    private StepConfiguration GetStepConfigById(string stepId) =>
        _config.Steps.FirstOrDefault(s => s.Id == stepId) ?? throw new InvalidOperationException($"Конфигурация для шага '{stepId}' не найдена.");

    private static MessageInfo UpdateConvertToMessageInfo(Update update) =>
        new()
        {
            ChatId = update.Message!.Chat.Id,
            Text = update.Message.Text,
            CallbackQueryChatId = update.CallbackQuery?.Message?.Chat.Id,
            Location = new ExternalServices.Models.Location
            {
                Latitude = update.Message.Location?.Latitude,
                Longitude = update.Message.Location?.Longitude
            }
        };
}
