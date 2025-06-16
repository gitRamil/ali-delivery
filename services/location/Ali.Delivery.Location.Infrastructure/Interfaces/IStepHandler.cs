using Ali.Delivery.Location.Infrastructure.ExternalServices.Models.Configuration;
using Telegram.Bot.Types;

namespace Ali.Delivery.Location.Infrastructure.Interfaces;

public interface IStepHandler
{
    Task<HandlerResult> HandleAsync(Update update);

    Task OnEnterAsync(long chatId);
}

public interface IStepHandlerMapping
{
    IStepHandler GetHandler(string stepId);
}
