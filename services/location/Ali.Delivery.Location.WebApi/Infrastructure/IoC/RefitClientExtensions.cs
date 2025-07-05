using Ali.Delivery.Location.Application.Abstractions;
using Refit;

namespace Ali.Delivery.Location.WebApi.Infrastructure.IoC;

/// <summary>
/// Предоставляет методы расширения для <see cref="IServiceCollection" /> для удобной регистрации клиентов Refit.
/// </summary>
public static class RefitClientExtensions
{
    /// <summary>
    /// Регистрирует и настраивает Refit-клиент для интерфейса <see cref="IFileServiceForOrder" />.
    /// Метод считывает базовый адрес (BaseAddress) для HTTP-клиента из конфигурации по ключу
    /// <c>ExternalServices:OrderService</c>.
    /// </summary>
    /// <param name="services">Коллекция сервисов для регистрации.</param>
    /// <param name="configuration">Конфигурация приложения, используемая для получения URL сервиса заказов.</param>
    /// <returns>
    /// Та же самая коллекция <see cref="IServiceCollection" /> для возможности построения цепочки вызовов.
    /// </returns>
    public static IServiceCollection AddRefitClientForOrderDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRefitClient<IFileServiceForOrder>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["ExternalServices:OrderService"] ?? throw new InvalidOperationException("Отсутствует строка подключения в ключе 'ExternalServices' для 'OrderService'")));

        return services;
    }
}
