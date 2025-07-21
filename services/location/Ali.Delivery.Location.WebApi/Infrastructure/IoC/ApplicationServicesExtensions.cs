using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Infrastructure.Services;

namespace Ali.Delivery.Location.WebApi.Infrastructure.IoC;

/// <summary>
/// Предоставляет методы расширения для регистрации сервисов приложения.
/// </summary>
public static class ApplicationServicesExtensions
{
    /// <summary>
    /// Регистрирует реализации сервисов приложения в контейнере зависимостей, включая сервис локализации уведомлений.
    /// </summary>
    /// <param name="services">Коллекция сервисов для регистрации.</param>
    /// <returns>Та же коллекция для построения цепочки вызовов.</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<INotificationLocalizationService, NotificationLocalizationService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IUserLanguageService, UserLanguageService>();

        return services;
    }
}
