namespace Ali.Delivery.Location.Infrastructure.Interfaces;

public interface IStepHandlerMapping
{
    IStepHandler GetHandler(string stepId);
}
