using Ali.Delivery.Location.Infrastructure.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Services;

public class TelegramBotService(ITelegramBotClient botClient, IStateMachine stateMachine, ILogger<TelegramBotService> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = [],
            DropPendingUpdates= true
        };

        botClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            errorHandler: HandleErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: stoppingToken
        );

        logger.LogInformation("Telegram bot started");
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    private async Task HandleUpdateAsync(
        ITelegramBotClient _, 
        Update update, 
        CancellationToken cancellationToken)
    {
        if (update.Message?.From?.Id is not { } userId)
        {
            return;
        }

        try
        {
            var result = await stateMachine.ProcessUpdateAsync(userId, update);
            
            if (!string.IsNullOrEmpty(result.ResponseMessage))
            {
                await botClient.SendMessage(
                    chatId: userId,
                    text: result.ResponseMessage,
                    cancellationToken: cancellationToken);
            }
        }
        catch (ApiRequestException ex)
        {
            logger.LogError(ex, "Telegram API Error: {ErrorCode}", ex.ErrorCode);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing update for user {UserId}", userId);
        }
    }

    private Task HandleErrorAsync(
        ITelegramBotClient _, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException 
                => $"Telegram API Error: {apiRequestException.ErrorCode}",
            _ => exception.ToString()
        };

        logger.LogError(errorMessage);
        return Task.CompletedTask;
    }
}


