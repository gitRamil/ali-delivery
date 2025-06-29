using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Ali.Delivery.Location.Infrastructure.Extensions;

/// <summary>
/// Предоставляет методы расширения для регистрации сервисов приложения.
/// </summary>
public static class ApplicationServicesExtensions
{
    /// <summary>
    /// Регистрирует реализации сервисов приложения в контейнере зависимостей.
    /// </summary>
    /// <param name="services">Коллекция сервисов для регистрации.</param>
    /// <returns>Та же коллекция для построения цепочки вызовов.</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<INotificationService, NotificationService>();

        return services;
    }
}
