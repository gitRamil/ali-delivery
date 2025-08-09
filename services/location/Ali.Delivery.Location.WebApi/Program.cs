using Ali.Delivery.Location.WebApi.Infrastructure.IoC;
using DotNetEnv;
using Hellang.Middleware.ProblemDetails;
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
    builder.Services.AddRabbitMqServices(configuration);
    builder.Services.AddRefitClientForOrderDb(configuration);
    builder.Services.AddPersistence(configuration);
    builder.Services.AddCommandHandlers();
    builder.Configuration.AddEnvironmentVariables("AliDeliveryLocationService_");
    builder.AddDefaultSerilog();
    builder.Services.AddMemoryCache();
    builder.Services.AddLazyCache();
    builder.Services.AddLogging();
    builder.Services.AddCustomJsonOptions();
    builder.Services.AddDefaultApiVersioning();
    builder.Services.AddDefaultSwagger();
    builder.Services.AddDefaultMediatr();
    builder.Services.AddDefaultEfCore();
    builder.Services.AddDefaultCorsPolicy();
    builder.Services.AddDateTimeService();
    builder.Services.AddDefaultProblemDetails();
    builder.Services.AddSwaggerGen();
    builder.Services.AddApplicationServices();


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