using Ali.Delivery.Location.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.BackgroundServices;

public sealed class TelegramBotService : BackgroundService
{
    private readonly ITelegramBotClient _botClient;
    private readonly ILogger<TelegramBotService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public TelegramBotService(ILogger<TelegramBotService> logger, ITelegramBotClient botClient, IServiceProvider serviceProvider)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _botClient = botClient ?? throw new ArgumentNullException(nameof(botClient));
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var me = await _botClient.GetMe(cancellationToken);
        _logger.LogInformation("Бот @{User} запущен", me.Username);

        var opts = new ReceiverOptions
        {
            AllowedUpdates = [], // все типы
            DropPendingUpdates = true
        };

        _botClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync, opts, cancellationToken);
    }

    private Task HandleErrorAsync(ITelegramBotClient bot, Exception ex, CancellationToken ct)
    {
        _logger.LogError(ex, "Ошибка в Telegram-поллинге");
        return Task.CompletedTask;
    }

    private async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken ct)
    {
        using var scope = _serviceProvider.CreateScope();
        var sm = scope.ServiceProvider.GetRequiredService<IStateMachine>();

        try
        {
            await sm.ProcessUpdateAsync(update);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка разбора Update {Id}", update.Id);
        }
    }
}
