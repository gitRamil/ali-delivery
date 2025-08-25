using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Handlers;
using Ali.Delivery.Order.Infrastructure.Persistence.Configurations.RabbitMqConfigurations;
using Ali.Delivery.Order.Infrastructure.Services;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Ali.Delivery.Order.WebApi.Infrastructure.IoC;

/// <summary>
/// Содержит набор методов расширения для регистрации служб RabbitMQ в контейнере внедрения зависимостей.
/// </summary>
internal static class RabbitMqServiceExtensions
{
    /// <summary>
    /// Добавляет службы RabbitMQ в контейнер внедрения зависимостей.
    /// </summary>
    /// <param name="services">Коллекция сервисов для регистрации.</param>
    /// <param name="configuration">Конфигурация приложения, используемая для получения данных подключения к RabbitMQ.</param>
    public static IServiceCollection AddRabbitMqService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqConfiguration>(configuration.GetSection("RabbitMq"));

        services.Configure<ExchangeConfiguration>(configuration.GetSection("RabbitMq:Exchanges"));
        services.Configure<QueueConfiguration>(configuration.GetSection("RabbitMq:Queues"));

        services.AddSingleton<RabbitMqConfiguration>(provider =>
        {
            var options = provider.GetRequiredService<IOptions<RabbitMqConfiguration>>();
            return options.Value;
        });

        services.AddSingleton<RabbitMqConnectionFactory>();

        services.AddSingleton<IConnection>(provider =>
        {
            var factory = provider.GetRequiredService<RabbitMqConnectionFactory>();
            return factory.CreateConnection();
        });
        services.AddSingleton<IMessageConsumer, RabbitMqConsumerService>();
        services.AddScoped<LocationCreatedHandler>();

        return services;
    }
}
