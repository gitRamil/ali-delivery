using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ali.Delivery.Domain.Core.Primitives;
using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Extensions;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Ali.Delivery.Order.Application.UseCases.RolePermission;

/// <summary>
/// Обработчик команды для создания разрешения для роли.
/// </summary>
public class CreateRolePermissionCommandHandler : IRequestHandler<CreateRolePermissionCommand, Guid>
{
    private readonly IConfiguration _configuration;
    private readonly IAppDbContext _context;

    /// <summary>
    /// Конструктор обработчика команды <see cref="CreateRolePermissionCommandHandler" />.
    /// </summary>
    /// <param name="context">Контекст приложения.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    public CreateRolePermissionCommandHandler(IAppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    /// <summary>
    /// Обрабатывает команду для создания связи между ролью и разрешением.
    /// </summary>
    /// <param name="request">Команда создания разрешения для роли.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Идентификатор созданной связи.</returns>
    public async Task<Guid> Handle(CreateRolePermissionCommand request, CancellationToken cancellationToken)
    {
        var permission = request.Permission.ToPermission();
        var role = request.Role.ToRole();

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"] ?? string.Empty);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("roleId", role.Id.ToString()),
                new Claim("permissionId", permission.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddDays(int.Parse(_configuration["JwtSettings:ExpirationDays"] ?? "7")),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

        var rolePermission = new Domain.Entities.RolePermission(SequentialGuid.Create(), permission, role, token);

        _context.RolePermissions.Add(rolePermission);
        await _context.SaveChangesAsync(cancellationToken);

        return rolePermission.Id;
    }
}
