using System.Text.Json;
using System.Text.Json.Serialization;
using Ali.Delivery.Location.Infrastructure;
using Ali.Delivery.Location.Infrastructure.BackgroundServices;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Ali.Delivery.Location.Infrastructure.Models.Configuration;
using Ali.Delivery.Location.Infrastructure.Services;
using Ali.Delivery.Location.Infrastructure.StateMachine;
using Ali.Delivery.Location.Infrastructure.StepHandlers;
using Ali.Delivery.Location.WebApi.Infrastructure.IoC;
using DotNetEnv;
using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;
using Refit;
using Serilog;
using Telegram.Bot;

try
{
    Env.Load();
    var builder = WebApplication.CreateBuilder(args);
    var configuration = builder.Configuration;

    builder.Services.AddEndpointsApiExplorer();
    builder.Configuration.AddJsonFile("stateTransitions.json", false, true);
    builder.Services.Configure<StateMachineConfiguration>(builder.Configuration);
    builder.Configuration.AddEnvironmentVariables("AliDeliveryLocationService_");
    builder.AddDefaultSerilog();
    builder.Services.AddMemoryCache();
    builder.Services.AddLogging();

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
    builder.Services.AddDateTimeService();
    builder.Services.AddDefaultProblemDetails();
    builder.Services.AddSwaggerGen();

    // Конфигурация Telegram бота
    var botToken = configuration["TelegramToken"];

    if (string.IsNullOrEmpty(botToken))
    {
        Log.Fatal("TelegramToken is not configured!");
        return 1;
    }

    builder.Services.AddSingleton<ITelegramBotClient>(_ => new TelegramBotClient(botToken));

    // State Machine и обработчики

    builder.Services.AddSingleton<IUserStateService, InMemoryUserStateService>();
    builder.Services.AddScoped<IStepHandlerMapping, StepHandlerMapping>();
    builder.Services.AddScoped<IStateMachine, StateMachine>();

    builder.Services.AddScoped<StartStepHandler>();
    builder.Services.AddScoped<LoginStepHandler>();
    builder.Services.AddScoped<AuthCompleteStepHandler>();
    builder.Services.AddScoped<GeosharingStepHandler>();
    builder.Services.AddScoped<StopStepHandler>();
    builder.Services.AddScoped<SaveLocationService>();

    builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString(nameof(AppDbContext)))
                                                                  .UseSnakeCaseNamingConvention()
                                                                  .EnableSensitiveDataLogging()
                                                                  .LogTo(Console.WriteLine, LogLevel.Information));

    // Внешние API сервисы
    builder.Services.AddRefitClient<IFileServiceForOrder>()
           .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["ExternalServices:OrderService"]!));
    
    // Сервисы приложения
    builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
    builder.Services.AddScoped<INotificationService, NotificationService>();

    // Фоновый сервис бота
    builder.Services.AddHostedService<TelegramBotService>();

    var app = builder.Build();
    app.AddAutomaticMigrations();
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseSerilogRequestLogging();
    // app.UseHttpsRedirection();
    app.UseProblemDetails();
    app.UseRouting();
    app.UseCors();
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
