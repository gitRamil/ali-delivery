using Ali.Delivery.Location.Infrastructure.ExternalServices.Models.Configuration;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Handlers;

public class LoginStepHandler(ICommandMethods method, ITelegramBotClient bot, INotificationService notification) : IStepHandler
{
    public async Task<HandlerResult> HandleAsync(Update update)
    {
        var chatId = update.Message?.Chat.Id ?? 0;

        if (chatId == 0)
        {
            return new HandlerResult
            {
                NextStepOption = ""
            };
        }

        if (update.Message is not { Text: { } text })
        {
            var hint = notification.GenerateNotificationMessage(NotificationType.N1_InvalidAuthCommand)!;
            await bot.SendMessage(chatId, hint);

            return new HandlerResult
            {
                NextStepOption = ""
            };
        }

        var res = await method.LoginAsync(chatId, text);

        return new HandlerResult
        {
            NextStepOption = res.NextStepKey
        };
    }

    public async Task OnEnterAsync(long chatId)
    {
        var prompt = notification.GenerateNotificationMessage(NotificationType.N0_EnterCredentials)!;
        await bot.SendMessage(chatId, prompt); // актуальный метод[1][2]
    }
}
