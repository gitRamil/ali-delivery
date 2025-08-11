using Ali.Delivery.Location.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Location.WebApi.Infrastructure.IoC;

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
        return services;
    }
}
