using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Infrastructure.Persistence.Configurations;
using Ali.Delivery.Location.Infrastructure.Services;
using RabbitMQ.Client;

namespace Ali.Delivery.Location.WebApi.Infrastructure.IoC;

/// <summary>
/// Содержит набор методов расширения для регистрации служб RabbitMQ в контейнере внедрения зависимостей.
/// </summary>
public static class RabbitMqServiceExtensions
{
    /// <summary>
    /// Добавляет службы RabbitMQ в контейнер внедрения зависимостей.
    /// </summary>
    /// <param name="services">Коллекция сервисов для регистрации.</param>
    /// <param name="configuration">Конфигурация приложения, используемая для получения данных подключения к RabbitMQ.</param>
    public static IServiceCollection AddRabbitMqServices(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqConfig = new RabbitMqConfiguration();
        configuration.GetSection("RabbitMQ").Bind(rabbitMqConfig);
        services.AddSingleton(rabbitMqConfig);

        services.AddSingleton<RabbitMqConnectionFactory>();

        services.AddSingleton<IConnection>(serviceProvider =>
        {
            var factory = serviceProvider.GetRequiredService<RabbitMqConnectionFactory>();
            return factory.CreateConnection();
        });

        services.AddScoped<IPublisherService, RabbitMqPublisherService>();

        return services;
    }
}