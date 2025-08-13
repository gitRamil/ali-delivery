using System.Text.Json;
using System.Text.Json.Serialization;
using Ali.Delivery.Order.Application;
using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Handlers;
using Ali.Delivery.Order.Application.Services;
using Ali.Delivery.Order.Infrastructure.services;
using Ali.Delivery.Order.WebApi.Infrastructure.IoC;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Configuration.AddEnvironmentVariables("AliDeliveryOrderService_");
    builder.AddDefaultSerilog();

    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        });
    builder.Services.AddDefaultApiVersioning();
    builder.Services.AddDefaultSwagger();
    builder.Services.AddDefaultMediatr();
    builder.Services.AddDefaultEfCore();
    builder.Services.AddDefaultCorsPolicy();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDateTimeService();
    builder.Services.AddDefaultProblemDetails();
    builder.Services.AddScoped<JwtProvider>();
    builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
    builder.Services.AddApiAuthentication();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddTransient<IDictionaryValuesProvider, DictionaryValuesProvider>();
    builder.Services.AddTransient<IDictionaryTypeMap, DictionaryTypeMap>();
    builder.Services.AddTransient<ICurrentUser, CurrentUserService>();

    // Конфигурация RabbitMQ
    builder.Services.Configure<RabbitMQConfiguration>(
        builder.Configuration.GetSection("RabbitMQ"));

// ИСПРАВЛЕНИЕ: Регистрируем RabbitMQConfiguration как singleton
    builder.Services.AddSingleton<RabbitMQConfiguration>(provider =>
    {
        var options = provider.GetRequiredService<IOptions<RabbitMQConfiguration>>();
        return options.Value;
    });

// RabbitMQConnectionFactory теперь сможет получить RabbitMQConfiguration
    builder.Services.AddSingleton<RabbitMQConnectionFactory>();

// RabbitMQ Connection
    builder.Services.AddSingleton<IConnection>(provider =>
    {
        var factory = provider.GetRequiredService<RabbitMQConnectionFactory>();
        return factory.CreateConnection();
    });

// Consumer
    builder.Services.AddSingleton<IMessageConsumer, RabbitMQConsumerService>();

// Handlers
    builder.Services.AddScoped<LocationCreatedHandler>();

    
    
    var app = builder.Build();
    app.AddAutomaticMigrations();

    // Запуск консумера
    var consumer = app.Services.GetRequiredService<IMessageConsumer>();
    await consumer.StartAsync();

// Остановка консумера при завершении приложения
    app.Lifetime.ApplicationStopping.Register(async () =>
    {
        await consumer.StopAsync();
    });

    //if (app.Environment.IsDevelopment())
    //{
    app.UseSwagger();
    app.UseSwaggerUI();
    //}

    app.UseSerilogRequestLogging();
    // app.UseHttpsRedirection();
    app.UseProblemDetails();
    app.UseRouting();
    app.UseCors();

    app.UseCookiePolicy(new CookiePolicyOptions
    {
        MinimumSameSitePolicy = SameSiteMode.Strict,
        HttpOnly = HttpOnlyPolicy.Always,
        Secure = CookieSecurePolicy.None // для http
    });
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();

    return 0;
}
catch (Exception e)
{
    Log.Fatal(e, "Хост неожиданно прекратил работу");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}