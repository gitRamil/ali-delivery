using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.BackgroundServices;

/// <summary>
/// Представляет фоновый сервис, который отвечает за постоянное получение (поллинг) обновлений от Telegram Bot API
/// и их последующую обработку.
/// </summary>
public sealed class TelegramBotService : BackgroundService
{
    private readonly ILogger<TelegramBotService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly ITelegramBotClient _telegramBotClient;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="TelegramBotService" />.
    /// </summary>
    /// <param name="logger">Логгер для записи событий и ошибок.</param>
    /// <param name="serviceProvider">
    /// Поставщик сервисов для создания Scoped-областей и разрешения зависимостей для каждого
    /// обновления.
    /// </param>
    /// <param name="telegramBotClient">Клиент для взаимодействия с Telegram Bot API.</param>
    public TelegramBotService(ILogger<TelegramBotService> logger, IServiceProvider serviceProvider, ITelegramBotClient telegramBotClient)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _telegramBotClient = telegramBotClient ?? throw new ArgumentNullException(nameof(telegramBotClient));
    }

    /// <inheritdoc />
    /// <remarks>
    /// Этот метод запускается один раз при старте приложения. Он выполняет начальную настройку,
    /// получает информацию о боте, конфигурирует опции приемника и запускает бесконечный цикл.
    /// </remarks>
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var me = await _telegramBotClient.GetMe(cancellationToken);
        _logger.LogInformation("Бот @{User} запущен", me.Username);

        var opts = new ReceiverOptions
        {
            AllowedUpdates = [],
            DropPendingUpdates = true
        };

        _telegramBotClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync, opts, cancellationToken);
    }

    private static MessageInfo ConvertUpdateToMessageInfo(Update update)
    {
        var chatId = update.Message?.Chat.Id ?? update.CallbackQuery?.Message?.Chat.Id ?? throw new InvalidOperationException("Не найден идентификатор чата.");
        var fromId = update.Message?.From?.Id ?? throw new InvalidOperationException("Не найден отправитель сообщения.");
        var location = update.Message?.Location != null ? new MessageLocation(update.Message.Location.Latitude, update.Message.Location.Longitude) : null;

        return new MessageInfo
        {
            ChatId = chatId,
            FromId = fromId,
            Text = update.Message?.Text,
            Location = location
        };
    }

    private Task HandleErrorAsync(ITelegramBotClient bot, Exception ex, CancellationToken cancellationToken)
    {
        _logger.LogError(ex, "Ошибка в Telegram-поллинге");
        return Task.CompletedTask;
    }

    private async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
        var stateMachine = scope.ServiceProvider.GetRequiredService<IStateMachine>();

        try
        {
            var messageInfo = ConvertUpdateToMessageInfo(update);

            Task SendInvalidCommandTelegramMessage() =>
                notificationService.SendNotificationMessageAsync(messageInfo.ChatId, NotificationType.N1_InvalidCommand, null, cancellationToken);

            await stateMachine.ProcessUpdateAsync(messageInfo, SendInvalidCommandTelegramMessage);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка разбора Update {Id}", update.Id);
        }
    }
}
