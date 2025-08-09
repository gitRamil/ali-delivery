using RabbitMQ.Client;

namespace Ali.Delivery.Location.Infrastructure.Services;

/// <summary>
/// Сервис для подключения к Rabbitmq.
/// </summary>
/// <param name="connection">Подключение.</param>
public class RabbitMqConnectionService(IConnection connection)
{
    public IConnection Connection { get; } = connection;
}