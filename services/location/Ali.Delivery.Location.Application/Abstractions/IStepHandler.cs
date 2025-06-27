using Ali.Delivery.Location.Application.Models;

namespace Ali.Delivery.Location.Application.Abstractions;

public interface IStepHandler
{
    Task<HandlerResult> HandleAsync(MessageInfo update);
}
