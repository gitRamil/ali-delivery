using Ali.Delivery.Location.Infrastructure.Handlers;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Ali.Delivery.Location.Infrastructure.ExternalServices.Models.Configuration;

public sealed class StepHandlerMapping : IStepHandlerMapping
{
    private readonly IReadOnlyDictionary<string, Type> _map;
    private readonly IServiceProvider _sp;

    public StepHandlerMapping(IServiceProvider sp)
    {
        _sp = sp ?? throw new ArgumentNullException(nameof(sp));

        _map = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
        {
            ["StartStep"] = typeof(StartStepHandler),
            ["Authorization"] = typeof(LoginStepHandler),
            ["AuthComplete"] = typeof(AuthCompleteStepHandler),
            ["GeoSharing"] = typeof(GeosharingStepHandler),
            ["StopStep"] = typeof(StopStepHandler)
        };
    }

    public IStepHandler GetHandler(string stepId)
    {
        if (!_map.TryGetValue(stepId, out var handlerType))
        {
            throw new KeyNotFoundException($"Handler not found for step '{stepId}'");
        }

        return (IStepHandler)_sp.GetRequiredService(handlerType);
    }
}
