using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Infrastructure.Persistence;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.WebApi.Infrastructure.IoC;

/// <summary>
/// Содержит набор методов расширения для регистрации служб Entity Framework в контейнере внедрения зависимостей.
/// </summary>
internal static class EntityFrameworkExtensions
{
    /// <summary>
    /// Добавляет автоматическую миграцию при запуске проекта.
    /// </summary>
    /// <param name="app">Конвеер запросов приложения.</param>
    public static IApplicationBuilder AddAutomaticMigrations(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                                    .CreateScope();
        using var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
        return app;
    }

    /// <summary>
    /// Добавляет службы Entity Framework в контейнер внедрения зависимостей.
    /// </summary>
    /// <param name="services">Коллекция дескрипторов службы.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="services" /> равен <c>null</c>.
    /// </exception>
    /// <returns>Коллекция дескрипторов службы.</returns>
    public static IServiceCollection AddDefaultEfCore(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(nameof(services));

        services.AddDbContext<AppDbContext>((provider, builder) =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();

            var connectionString = configuration.GetConnectionString(DbConstants.ConnectionStringSectionName) ??
                                   throw new ArgumentException("Строка подключения к БД не указана в конфигурации.");
            builder.UseExceptionProcessor();
            builder.UseLazyLoadingProxies();
            builder.UseSnakeCaseNamingConvention();
            builder.UseNpgsql(connectionString, optionsBuilder => optionsBuilder.EnableRetryOnFailure());
            builder.UseLoggerFactory(LoggerFactory.Create(l => l.AddConsole()));
        });
        services.AddScoped<IAppDbContext, AppDbContext>();
        return services;
    }
}
