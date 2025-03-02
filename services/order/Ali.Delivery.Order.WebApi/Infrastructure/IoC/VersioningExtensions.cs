using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace Ali.Delivery.Order.WebApi.Infrastructure.IoC;

/// <summary>
/// Содержит набор методов расширения для регистрации служб версионирования в контейнере внедрения зависимостей.
/// </summary>
internal static class VersioningExtensions
{
    /// <summary>
    /// Добавляет службы версионирования в контейнер внедрения зависимостей.
    /// </summary>
    /// <param name="services">Коллекция дескрипторов службы.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="services" /> равен <c>null</c>.
    /// </exception>
    /// <returns>Коллекция дескрипторов службы.</returns>
    public static IServiceCollection AddDefaultApiVersioning(this IServiceCollection services)
    {
        services.AddRouting(options => options.LowercaseUrls = true);

        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.DefaultApiVersion = ApiVersion.Default;
            options.ErrorResponses = new ProblemDetailsErrorResponseProvider();
        });
        services.AddVersionedApiExplorer(setup => setup.SubstituteApiVersionInUrl = true);

        return services;
    }
}
