namespace Ali.Delivery.Location.Application.Abstractions;

public interface IPublisherService
{
    Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default);
    Task PublishAsync<TMessage>(TMessage message, string routingKey, CancellationToken cancellationToken = default);
    Task PublishAsync<TMessage>(TMessage message, string exchange, string routingKey, CancellationToken cancellationToken = default);
}
