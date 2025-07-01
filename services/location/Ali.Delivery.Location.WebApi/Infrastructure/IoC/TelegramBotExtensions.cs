using Ali.Delivery.Location.Infrastructure.BackgroundServices;
using Telegram.Bot;

namespace Ali.Delivery.Location.WebApi.Infrastructure.IoC;

/// <summary>
/// Предоставляет методы расширения для регистрации сервисов, связанных с Telegram ботом.
/// </summary>
public static class TelegramBotExtensions
{
    /// <summary>
    /// Регистрирует ITelegramBotClient и фоновый сервис TelegramBotService.
    /// Выбрасывает исключение, если токен не найден в конфигурации.
    /// </summary>
    /// <param name="services">Коллекция сервисов для регистрации.</param>
    /// <param name="configuration">Конфигурация приложения для получения токена.</param>
    /// <returns>Та же коллекция для построения цепочки вызовов.</returns>
    /// <exception cref="InvalidOperationException">
    /// Возникает, если значение "TelegramToken" отсутствует или является пустой строкой в конфигурации.
    /// </exception>
    public static IServiceCollection AddTelegramBotService(this IServiceCollection services, IConfiguration configuration)
    {
        var botToken = configuration["TelegramToken"];

        if (string.IsNullOrEmpty(botToken))
        {
            throw new InvalidOperationException("Критическая ошибка: TelegramToken не задан в конфигурации. Приложение не может быть запущено.");
        }

        services.AddSingleton<ITelegramBotClient>(_ => new TelegramBotClient(botToken));
        services.AddHostedService<TelegramBotService>();

        return services;
    }
}
