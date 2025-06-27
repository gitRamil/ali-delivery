namespace Ali.Delivery.Location.Application.Abstractions;

public interface IStepHandlerMapping
{
    IStepHandler GetHandler(string stepId);
}
