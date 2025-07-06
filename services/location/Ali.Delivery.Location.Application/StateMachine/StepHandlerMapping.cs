using System.Collections.ObjectModel;
using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Constants;
using Ali.Delivery.Location.Application.StepHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace Ali.Delivery.Location.Application.StateMachine;

/// <summary>
/// Представляет реализацию, которая сопоставляет строковый идентификатор шага (состояния)
/// с его конкретным обработчиком <see cref="IStepHandler" />.
/// Эта реализация использует <see cref="IServiceProvider" /> для динамического разрешения
/// экземпляров обработчиков во время выполнения.
/// </summary>
public sealed class StepHandlerMapping : IStepHandlerMapping
{
    private readonly ReadOnlyDictionary<string, Type> _map = new(new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
    {
        [Steps.Start] = typeof(StartStepHandler),
        [Steps.Authorization] = typeof(LoginStepHandler),
        [Steps.AuthComplete] = typeof(AuthCompleteStepHandler),
        [Steps.GeoSharing] = typeof(GeosharingStepHandler),
        [Steps.Stop] = typeof(StopStepHandler),
        [Steps.LanguageSelection] = typeof(LanguageSelectionStepHandler)
    });

    private readonly IServiceProvider _sp;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="StepHandlerMapping" />.
    /// </summary>
    /// <param name="sp">
    /// Поставщик сервисов (<see cref="IServiceProvider" />), используемый для получения экземпляров
    /// обработчиков из DI-контейнера.
    /// </param>
    public StepHandlerMapping(IServiceProvider sp) => _sp = sp ?? throw new ArgumentNullException(nameof(sp));

    /// <inheritdoc />
    /// <exception cref="KeyNotFoundException">
    /// Возникает, если для указанного <paramref name="stepId" /> не найдено сопоставление в словаре обработчиков.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Возникает от <see cref="ServiceProviderServiceExtensions.GetRequiredService" />, если тип обработчика найден в словаре,
    /// но не зарегистрирован в контейнере зависимостей.
    /// </exception>
    public IStepHandler GetHandler(string stepId)
    {
        if (!_map.TryGetValue(stepId, out var handlerType))
        {
            throw new KeyNotFoundException($"Handler not found for step '{stepId}'");
        }

        return (IStepHandler)_sp.GetRequiredService(handlerType);
    }
}
