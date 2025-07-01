using Ali.Delivery.Location.Application.StepHandlers;

namespace Ali.Delivery.Location.WebApi.Infrastructure.IoC;

/// <summary>
/// Предоставляет методы расширения StepHandlers для <see cref="IServiceCollection" />
/// </summary>
public static class StepHandlersExtensions
{
    /// <summary>
    /// Регистрирует все реализации обработчиков шагов "IStepHandler"
    /// в контейнере зависимостей с временем жизни Scoped.
    /// </summary>
    /// <param name="services">Коллекция сервисов для регистрации.</param>
    /// <returns>
    /// Та же самая коллекция <see cref="IServiceCollection" /> для возможности построения цепочки вызовов.
    /// </returns>
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        services.AddScoped<StartStepHandler>();
        services.AddScoped<LoginStepHandler>();
        services.AddScoped<AuthCompleteStepHandler>();
        services.AddScoped<GeosharingStepHandler>();
        services.AddScoped<StopStepHandler>();

        return services;
    }
}
