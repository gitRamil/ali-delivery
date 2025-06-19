using Ali.Delivery.Location.Infrastructure.Models;

namespace Ali.Delivery.Location.Infrastructure.Interfaces;

public interface IStepHandler
{
    Task<HandlerResult> HandleAsync(MessageInfo update);
}
