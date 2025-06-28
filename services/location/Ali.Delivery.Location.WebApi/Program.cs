using System.Text.Json;
using System.Text.Json.Serialization;
using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.Extensions;
using Ali.Delivery.Location.Application.Services;
using Ali.Delivery.Location.Infrastructure.Extensions;
using Ali.Delivery.Location.Infrastructure.Persistence;
using Ali.Delivery.Location.Infrastructure.Services;
using Ali.Delivery.Location.WebApi.Infrastructure.IoC;
using DotNetEnv;
using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;
using Refit;
using Serilog;

try
{
    Env.Load();
    var builder = WebApplication.CreateBuilder(args);
    var configuration = builder.Configuration;

    builder.Services.AddEndpointsApiExplorer();
    builder.Configuration.AddJsonFile("stateTransitions.json", false, true);
    builder.Services.AddTelegramBotService(configuration);
    builder.Services.AddStateMachine(configuration);
    builder.Services.AddCommandHandlers();
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
    
    builder.Services.AddScoped<IUserLocationRepository, UserLocationRepository>();
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
