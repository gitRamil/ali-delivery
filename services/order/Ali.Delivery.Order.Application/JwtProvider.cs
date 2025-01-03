using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ali.Delivery.Order.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Ali.Delivery.Order.Application;

/// <summary>
/// </summary>
public class JwtProvider
{
    private readonly JwtSettings _settings;

    /// <summary>
    /// </summary>
    /// <param name="options"></param>
    public JwtProvider(IOptions<JwtSettings> options) => _settings = options.Value;

    /// <summary>
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public string GenerateToken(User user)
    {
        Claim[] claims = [new Claim("userId", user.Id.ToString())];

        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("4654sdfg4sdfvsd-asdfasd152adf-asdfgasdf")),
                                                        SecurityAlgorithms.HmacSha256Signature);
        var token = new JwtSecurityToken(claims: claims, signingCredentials: signingCredentials, expires: DateTime.Now.AddHours(_settings.ExpitesHours));
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}
