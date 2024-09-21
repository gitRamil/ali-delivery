using Ali.Delivery.Order.Application.Exceptions;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace Ali.Delivery.Order.WebApi.IoC;

/// <summary>
/// Содержит набор методов расширения для регистрации служб ProblemDetails в контейнере внедрения зависимостей.
/// </summary>
internal static class ProblemDetailsExtensions
{
    /// <summary>
    /// Добавляет службы ProblemDetails в контейнер внедрения зависимостей.
    /// </summary>
    /// <param name="services">Коллекция дескрипторов службы.</param>
    /// <exception cref="System.ArgumentNullException">
    /// Возникает, если <paramref name="services" /> равен <c>null</c>.
    /// </exception>
    /// <returns>Коллекция дескрипторов службы.</returns>
    public static IServiceCollection AddDefaultProblemDetails(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        // Todo Локализация ответов стандартных ProblemDetails.
        services.AddProblemDetails(options =>
        {
            options.GetTraceId = context => context.TraceIdentifier;
            options.ShouldLogUnhandledException = (_, exception, _) => exception is not (ValidationException or NotFoundException);

            options.IncludeExceptionDetails = (context, _) =>
            {
                var webHostEnvironment = context.RequestServices.GetRequiredService<IWebHostEnvironment>();
                return webHostEnvironment.IsDevelopment() || webHostEnvironment.IsStaging();
            };

            options.Map<ValidationException>((context, exception) =>
            {
                var logger = context.RequestServices.GetRequiredService<ILogger<ProblemDetails>>();
                logger.LogError(exception, "Ошибка проверки достоверности");

                return new ValidationProblemDetails(exception.Errors)
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Status = StatusCodes.Status400BadRequest
                };
            });

            options.Map<NotFoundException>((context, exception) =>
            {
                var logger = context.RequestServices.GetRequiredService<ILogger<ProblemDetails>>();
                logger.LogError(exception, "Сущность не найдена");

                return new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                    Title = "The specified resource was not found.",
                    Detail = exception.Details,
                    Status = StatusCodes.Status404NotFound
                };
            });

            // TODO переделать на подходящее доменное исключение
            options.Map<InvalidOperationException>((_, e) => e.Data.Contains("DomainError"),
                                                   (context, exception) =>
                                                   {
                                                       var logger = context.RequestServices.GetRequiredService<ILogger<ProblemDetails>>();
                                                       logger.LogError(exception, "Ошибка уровня домена");

                                                       return new ProblemDetails
                                                       {
                                                           Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                                                           Title = exception.Message,
                                                           Status = StatusCodes.Status400BadRequest
                                                       };
                                                   });
        });
        return services;
    }
}
