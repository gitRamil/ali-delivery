using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpOverrides;

namespace Ali.Delivery.Order.WebApi.IoC;

/// <summary>
/// Содержит набор методов расширения для регистрации служб MVC в контейнере внедрения зависимостей.
/// </summary>
internal static class ControllersExtensions
{
    /// <summary>
    /// Добавляет службы MVC в контейнер внедрения зависимостей.
    /// </summary>
    /// <param name="services">Коллекция дескрипторов службы.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="services" /> равен <c>null</c>.
    /// </exception>
    /// <returns>Коллекция дескрипторов службы.</returns>
    public static IServiceCollection AddDefaultControllers(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(nameof(services));

        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.All;
            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();
        });

        services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                });

        return services;
    }
}
