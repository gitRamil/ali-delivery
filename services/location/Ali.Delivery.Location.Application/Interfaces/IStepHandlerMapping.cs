namespace Ali.Delivery.Location.Application.Interfaces;

public interface IStepHandlerMapping
{
    IStepHandler GetHandler(string stepId);
}
