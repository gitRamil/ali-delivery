using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Services;

namespace Ali.Delivery.Order.WebApi.IoC;

/// <summary>
/// Содержит набор методов расширения для регистрации служб уровня инфраструктуры в контейнере внедрения зависимостей.
/// </summary>
public static class DateTimeServiceExtensions
{
    /// <summary>
    /// Добавляет сервис дат уровня инфраструктуры в контейнер внедрения зависимостей.
    /// </summary>
    /// <param name="services">Коллекция дескрипторов службы.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="services" /> равен <c>null</c>.
    /// </exception>
    public static IServiceCollection AddDateTimeService(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddTransient<IDateTimeService, DateTimeService>();
        return services;
    }
}
