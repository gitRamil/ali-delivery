using Ali.Delivery.Order.Application;
using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Services;
using Ali.Delivery.Order.WebApi.Infrastructure.IoC;
using Ali.Delivery.Order.WebApi.IoC;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.Extensions.Options;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);
    var config = builder.Configuration;
    builder.AddDefaultSerilog();
    builder.Services.AddControllers();
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
    builder.Services.Configure<JwtOptions>(config.GetSection(nameof(JwtOptions)));

    var jwtOptions = builder.Services.BuildServiceProvider()
                            .GetRequiredService<IOptions<JwtOptions>>();
    builder.Services.AddApiAuthentication(jwtOptions);
    
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddTransient<ICurrentUser, CurrentUserService>();

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

    app.UseCookiePolicy(new CookiePolicyOptions
    {
        MinimumSameSitePolicy = SameSiteMode.Strict,
        HttpOnly = HttpOnlyPolicy.Always,
        Secure = CookieSecurePolicy.Always
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
