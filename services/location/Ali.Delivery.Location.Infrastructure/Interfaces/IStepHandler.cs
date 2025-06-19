using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;
using Ali.Delivery.Location.Infrastructure.ExternalServices.Models.Configuration;

namespace Ali.Delivery.Location.Infrastructure.Interfaces;

public interface IStepHandler
{
    Task<HandlerResult> HandleAsync(MessageInfo update);
}

public interface IStepHandlerMapping
{
    IStepHandler GetHandler(string stepId);
}
