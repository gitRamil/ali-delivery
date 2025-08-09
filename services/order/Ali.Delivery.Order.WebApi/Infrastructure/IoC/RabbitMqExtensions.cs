using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Infrastructure.BackgroundServices;
using Ali.Delivery.Order.Infrastructure.services;
using Ali.Delivery.Order.Infrastructure.Services;
using RabbitMQ.Client;

namespace Ali.Delivery.Order.WebApi.Infrastructure.IoC;

/// <summary>
/// Содержит набор методов расширения для регистрации служб RabbitMQ в контейнере внедрения зависимостей.
/// </summary>
public static class RabbitMqExtensions
{
    /// <summary>
    /// Добавляет службы RabbitMQ в контейнер внедрения зависимостей.
    /// </summary>
    /// <param name="services">Коллекция сервисов для регистрации.</param>3
    /// <param name="configuration">Конфигурация приложения, используемая для получения данных подключения к RabbitMQ.</param>
    public static IServiceCollection AddRabbitMqServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<RabbitMqConnectionService>(_ =>
        {
            var factory = new ConnectionFactory()
            {
                HostName = configuration.GetValue<string>("RabbitMQ:HostName") ?? "localhost",
                UserName = configuration.GetValue<string>("RabbitMQ:UserName") ?? "guest",
                Password = configuration.GetValue<string>("RabbitMQ:Password") ?? "guest",
                Port = configuration.GetValue("RabbitMQ:Port", 5672)
            };

            var connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
            return new RabbitMqConnectionService(connection);
        });

        services.AddSingleton<ILocationStorageService, InMemoryLocationStorageService>();

        services.AddHostedService<LocationConsumerBackgroundService>();

        return services;
    }
}