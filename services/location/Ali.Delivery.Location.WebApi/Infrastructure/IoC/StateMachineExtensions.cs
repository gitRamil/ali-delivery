using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Configurations;
using Ali.Delivery.Location.Application.StateMachine;
using Ali.Delivery.Location.Infrastructure.Services;

namespace Ali.Delivery.Location.WebApi.Infrastructure.IoC;

/// <summary>
/// Предоставляет методы расширения для <see cref="IServiceCollection"/>
/// для регистрации сервисов, связанных с конечным автоматом (State Machine).
/// </summary>
public static class StateMachineExtensions
{
    /// <summary>
    /// Регистрирует все необходимые сервисы для работы конечного автомата в контейнере зависимостей.
    /// Включает в себя конфигурацию, сервис состояния пользователя, маппинг обработчиков и сам конечный автомат.
    /// </summary>
    /// <param name="services">Коллекция сервисов для регистрации.</param>
    /// <param name="configuration">Конфигурация приложения, из которой будут считываться настройки конечного автомата.</param>
    /// <returns>
    /// Та же самая коллекция <see cref="IServiceCollection"/> для возможности построения цепочки вызовов.
    /// </returns>
    public static IServiceCollection AddStateMachine(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<StateMachineConfiguration>(configuration);
        services.AddSingleton<IUserStateService, InMemoryUserStateService>();
        services.AddScoped<IStepHandlerMapping, StepHandlerMapping>();
        services.AddScoped<IStateMachine, StateMachine>();
    
        return services;
    }
}
