using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ali.Delivery.Order.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Ali.Delivery.Order.Application;

/// <summary>
/// Предоставляет функциональность для создания JWT-токенов.
/// </summary>
public class JwtProvider
{
    private readonly JwtOptions _options;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="JwtProvider"/>.
    /// </summary>
    /// <param name="options">Настройки JWT, предоставленные через внедрение зависимостей.</param>
    public JwtProvider(IOptions<JwtSettings> options) => _settings = options.Value;

    /// <summary>
    /// Генерирует JSON Web Token (JWT) для указанного пользователя.
    /// </summary>
    /// <param name="user">Пользователь, для которого создается токен.</param>
    /// <returns>JWT в виде строки.</returns>
    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new("userId", user.Id.ToString()),
        };
        
        foreach (var permission in permissions)
        {
            claims.Add(new Claim("userPermissions", permission));
        }

        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key)),
                                                        SecurityAlgorithms.HmacSha256Signature);
        
        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.Now.AddHours(_settings.ExpitesHours)
        );
        
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}
