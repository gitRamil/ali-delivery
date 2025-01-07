using System.Text;
using Ali.Delivery.Order.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Ali.Delivery.Order.WebApi.Infrastructure.IoC;

/// <summary>
/// </summary>
public static class AuthExtensions
{
    /// <summary>
    /// </summary>
    /// <param name="services"></param>
    public static void AddApiAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
                              options =>
                              {
                                  var serviceProvider = services.BuildServiceProvider();
                                  var jwtOptions = serviceProvider.GetRequiredService<IOptions<JwtOptions>>();

                                  options.TokenValidationParameters = new TokenValidationParameters
                                  {
                                      ValidateIssuer = false,
                                      ValidateAudience = false,
                                      ValidateLifetime = true,
                                      ValidateIssuerSigningKey = true,
                                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey))
                                  };

                                  options.Events = new JwtBearerEvents
                                  {
                                      OnMessageReceived = context =>
                                      {
                                          context.Token = context.Request.Cookies["token"];
                                          return Task.CompletedTask;
                                      }
                                  };
                              });
    }
}
