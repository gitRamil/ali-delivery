using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Hubs;
using Ali.Delivery.Order.Infrastructure.Services;

namespace Ali.Delivery.Order.WebApi.Infrastructure.IoC;

/// <summary>
/// Содержит набор методов расширения для регистрации служб SignalR в контейнере внедрения зависимостей.
/// </summary>
internal static class SignalRServiceExtensions
{
    /// <summary>
    /// Добавляет CORS политику для SignalR.
    /// </summary>
    /// <param name="services">Коллекция сервисов для регистрации.</param>
    /// <param name="allowedOrigins">Разрешенные источники. По умолчанию - localhost:3000.</param>
    public static IServiceCollection AddSignalRCorsPolicy(this IServiceCollection services, params string[] allowedOrigins)
    {
        var defaultOrigins = allowedOrigins.Length > 0 ? allowedOrigins : ["http://localhost:3000", "https://localhost:3000"];

        services.AddCors(options =>
        {
            options.AddPolicy("SignalRCorsPolicy",
                              policy =>
                              {
                                  policy.WithOrigins(defaultOrigins)
                                        .AllowAnyMethod()
                                        .AllowAnyHeader()
                                        .AllowCredentials();
                              });
        });

        return services;
    }

    /// <summary>
    /// Добавляет службы SignalR в контейнер внедрения зависимостей.
    /// </summary>
    /// <param name="services">Коллекция сервисов для регистрации.</param>
    /// <param name="configuration">Конфигурация приложения (опционально для будущих настроек).</param>
    public static IServiceCollection AddSignalRServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSignalR(options =>
        {
            options.EnableDetailedErrors = true;
            options.KeepAliveInterval = TimeSpan.FromSeconds(15);
            options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
        });

        services.AddScoped<ILocationNotificationService, LocationNotificationService>();

        return services;
    }

    /// <summary>
    /// Настраивает маршруты хабов SignalR.
    /// </summary>
    /// <param name="app">Экземпляр приложения.</param>
    public static WebApplication MapSignalRHubs(this WebApplication app)
    {
        app.MapHub<LocationHub>("/locationHub");

        return app;
    }
}
