using Ali.Delivery.Location.Application.Models;
using Ali.Delivery.Location.Application.StepHandlers;

namespace Ali.Delivery.Location.Application.Interfaces;

public interface IStepHandler
{
    Task<HandlerResult> HandleAsync(MessageInfo update);
}
