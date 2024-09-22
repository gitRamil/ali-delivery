namespace Ali.Delivery.Order.WebApi.Infrastructure.IoC;

/// <summary>
/// Содержит набор методов расширения для регистрации служб Cors в контейнере внедрения зависимостей.
/// </summary>
internal static class CorsExtensions
{
    /// <summary>
    /// Добавляет службы Cors в контейнер внедрения зависимостей.
    /// </summary>
    /// <param name="services">Коллекция дескрипторов службы.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="services" /> равен <c>null</c>.
    /// </exception>
    /// <returns>Коллекция дескрипторов службы.</returns>
    public static IServiceCollection AddDefaultCorsPolicy(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(nameof(services));

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policyBuilder =>
            {
                policyBuilder.AllowAnyOrigin()
                             .AllowAnyHeader()
                             .AllowAnyMethod();
            });
        });
        return services;
    }
}
