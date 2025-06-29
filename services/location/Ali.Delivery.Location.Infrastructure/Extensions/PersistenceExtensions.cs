using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Services;
using Ali.Delivery.Location.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Ali.Delivery.Location.Infrastructure.Extensions;

/// <summary>
/// Предоставляет методы расширения для настройки слоя постоянного хранения данных (Persistence).
/// </summary>
public static class PersistenceExtensions
{
    /// <summary>
    /// Регистрирует DbContext и репозитории приложения в контейнере зависимостей.
    /// </summary>
    /// <param name="services">Коллекция сервисов для регистрации.</param>
    /// <param name="configuration">Конфигурация приложения для получения строки подключения.</param>
    /// <returns>Та же коллекция для построения цепочки вызовов.</returns>
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString(nameof(AppDbContext)))
                                                              .UseSnakeCaseNamingConvention()
                                                              .EnableSensitiveDataLogging()
                                                              .LogTo(Console.WriteLine, LogLevel.Information));
        
        services.AddScoped<IUserLocationRepository, UserLocationRepository>();

        return services;
    }
}
