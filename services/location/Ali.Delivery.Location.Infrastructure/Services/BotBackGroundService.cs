using Ali.Delivery.Location.Infrastructure.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Services;

public class BotBackgroundService(ITelegramBotClient botClient, IStateMachine stateMachine, ILogger<BotBackgroundService> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        botClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            cancellationToken: cancellationToken
        );
        
        logger.LogInformation("Bot started successfully");
        
        // Для предотвращения завершения задачи
        await Task.Delay(Timeout.Infinite, cancellationToken);
    }

    private async Task HandleUpdateAsync(
        ITelegramBotClient _, 
        Update update, 
        CancellationToken token)
    {
        if (update.Message?.From?.Id is { } userId)
        {
            try
            {
                var result = await stateMachine.ProcessUpdateAsync(userId, update);

                if (!string.IsNullOrEmpty(result.ResponseMessage))
                {
                    await botClient.SendMessage(userId, result.ResponseMessage, cancellationToken: token);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing update for user {UserId}", userId);
            }
        }
    }

    private Task HandleErrorAsync(
        ITelegramBotClient _, 
        Exception exception, 
        CancellationToken token)
    {
        logger.LogError(exception, "Telegram bot error occurred");
        return Task.CompletedTask;
    }
}