using System.Text.Json;
using System.Text.Json.Serialization;
using Ali.Delivery.Location.Infrastructure;
using Ali.Delivery.Location.Infrastructure.ExternalServices.Models.Configuration;
using Ali.Delivery.Location.Infrastructure.Handlers;
using Ali.Delivery.Location.Infrastructure.Interfaces;
using Ali.Delivery.Location.Infrastructure.Interfaces2._0;
using Ali.Delivery.Location.Infrastructure.Services;
using Ali.Delivery.Location.Infrastructure.Services.WriteToDataBase;
using Ali.Delivery.Location.Infrastructure.Services2._0;
using Ali.Delivery.Location.WebApi.Infrastructure.IoC;
using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;
using Refit;
using Serilog;
using Telegram.Bot;

try
{
    DotNetEnv.Env.Load(); 
    var builder = WebApplication.CreateBuilder(args);
    var configuration = builder.Configuration;
    
    builder.Services.AddEndpointsApiExplorer();
    builder.Configuration.AddJsonFile("stateTransitions.json", optional: false, reloadOnChange: true);
    builder.Services.Configure<StateTransitionsConfig>(builder.Configuration);
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
    
    // builder.Services.AddSingleton<IMyConfigurationService>(_ => new MyConfigurationService(builder.Configuration));
    
    // Конфигурация Telegram бота
    var botToken = configuration["BOT_TOKEN"];
    Console.WriteLine($"Bot Token: {botToken}");
    if (string.IsNullOrEmpty(botToken))
    {
        // Можно добавить более строгую обработку, если токен критичен
        Log.Fatal("BOT_TOKEN is not configured!"); 
        return 1; // Завершить приложение, если токен не найден
    }
    builder.Services.AddSingleton<ITelegramBotClient>(_ => 
                                                          new TelegramBotClient(botToken));
    
    // State Machine и обработчики
    
    builder.Services.AddScoped<IStateManager, StateManager>();
    builder.Services.AddScoped<IUserDataManager, UserDataManager>();
    builder.Services.AddScoped<ITransitionResolver, TransitionResolver>();
    builder.Services.AddScoped<INotificationService, NotificationService>();
    builder.Services.AddScoped<IUpdateProcessor, UpdateProcessor>();
    builder.Services.AddScoped<IStateMachine, StateMachineOrchestrator>();
    
    builder.Services.AddScoped<ICommandHandler, LoginCommandHandler>();
    builder.Services.AddScoped<ICommandHandler, CredentialsHandler>();
    builder.Services.AddScoped<ICommandHandler, GeosharingCommandHandler>();
    builder.Services.AddScoped<ICommandHandler, LocationHandler>();
    builder.Services.AddScoped<ICommandHandler, StopCommandHandler>();
    builder.Services.AddScoped<ICommandHandler, StartCommandHandler>();
    
    builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString(nameof(AppDbContext)))
                                                                  .UseSnakeCaseNamingConvention()
                                                                  .EnableSensitiveDataLogging()
                                                                  .LogTo(Console.WriteLine, LogLevel.Information));

    // Внешние API сервисы
    builder.Services.AddRefitClient<IFileServiceForOrder>()
           .ConfigureHttpClient(c => 
                                    c.BaseAddress = new Uri(configuration["ExternalServices:OrderService"]!));
    
    builder.Services.AddRefitClient<IFileServiceForLocation>()
           .ConfigureHttpClient(c => 
                                    c.BaseAddress = new Uri(configuration["ExternalServices:LocationService"]!));
    
    // Сервисы приложения
    builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
    builder.Services.AddScoped<ILocationService, LocationService>();
    builder.Services.AddScoped<IWriteToDatabase, WriteToDatabase>();

    // Фоновый сервис бота
    builder.Services.AddHostedService<BotBackgroundService>();
    
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
