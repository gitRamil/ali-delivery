using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ali.Delivery.Location.WebApi.Infrastructure.IoC;

/// <summary>
/// Предоставляет методы расширения для настройки сериализации.
/// </summary>
public static class SerializationExtensions
{
    /// <summary>
    /// Добавляет и настраивает сервисы контроллеров с кастомными опциями для JSON.
    /// Включает конвертер для перечислений (Enum) в camelCase строки.
    /// </summary>
    /// <param name="services">Коллекция сервисов для регистрации.</param>
    /// <returns>Та же самая коллекция для построения цепочки вызовов.</returns>
    public static IServiceCollection AddCustomJsonOptions(this IServiceCollection services)
    {
        services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                });

        return services;
    }
}
