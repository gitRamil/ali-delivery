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
    /// Инициализирует новый экземпляр класса <see cref="JwtProvider" />.
    /// </summary>
    /// <param name="options">Настройки JWT, предоставленные через внедрение зависимостей.</param>
    public JwtProvider(IOptions<JwtOptions> options) => _options = options.Value;

    /// <summary>
    /// Генерирует JSON Web Token (JWT) для указанного пользователя.
    /// </summary>
    /// <param name="user">Пользователь, для которого создается токен.</param>
    /// <param name="permissions"></param>
    /// <returns>JWT в виде строки.</returns>
    public string GenerateToken(User user, IEnumerable<int> permissions)
    {
        var claims = new List<Claim>
        {
            new("userId", user.Id.ToString())
        };

        claims.AddRange(permissions.Select(permission => new Claim("userPermissions", permission.ToString())));

        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)), SecurityAlgorithms.HmacSha256Signature);
        var token = new JwtSecurityToken(claims: claims, signingCredentials: signingCredentials, expires: DateTime.Now.AddHours(_options.ExpiresHours));
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}
