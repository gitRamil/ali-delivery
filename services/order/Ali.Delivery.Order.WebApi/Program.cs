using Ali.Delivery.Order.WebApi.Infrastructure.IoC;
using Ali.Delivery.Order.WebApi.IoC;
using Hellang.Middleware.ProblemDetails;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.AddDefaultSerilog();
    builder.Services.AddControllers();
    builder.Services.AddDefaultApiVersioning();
    builder.Services.AddDefaultMediatr();
    builder.Services.AddDefaultEfCore();
    builder.Services.AddDefaultCorsPolicy();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDateTimeService();
    builder.Services.AddDefaultProblemDetails();

    var app = builder.Build();
    app.AddAutomaticMigrations();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();
    app.UseProblemDetails();
    app.UseRouting();
    app.UseCors();
    app.UseHttpsRedirection();
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
