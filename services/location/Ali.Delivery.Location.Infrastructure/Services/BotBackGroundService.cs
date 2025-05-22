using Ali.Delivery.Location.Infrastructure.Services.MyBotClient;
using Microsoft.Extensions.Hosting;

namespace Ali.Delivery.Location.Infrastructure.Services;

public class BotBackgroundService : BackgroundService
{
    private readonly IMyBotClient _botClient;

    public BotBackgroundService(IMyBotClient botClient)
    {
        _botClient = botClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
#pragma warning disable CS0618
        _botClient.RunBot();
#pragma warning restore CS0618
        Console.WriteLine("BotStarted");
    }
}