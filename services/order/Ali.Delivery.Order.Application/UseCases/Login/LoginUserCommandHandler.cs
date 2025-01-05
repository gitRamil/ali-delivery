using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Exceptions;
using Ali.Delivery.Order.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.Login;

/// <summary>
/// fhfghfg
/// </summary>
public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly IAppDbContext _dbcontext;
    private readonly JwtProvider _jwtProvider;

    /// <summary>
    /// fdagasdf
    /// </summary>
    /// <param name="jwtProvider"></param>
    /// <param name="dbcontext"></param>
    public LoginCommandHandler(JwtProvider jwtProvider, IAppDbContext dbcontext)
    {
        _jwtProvider = jwtProvider;
        _dbcontext = dbcontext;
    }

    /// <summary>
    /// ываываыва
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="UnauthorizedAccessException"></exception>
    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Поиск пользователя по логину
        var user = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Login == request.Login, cancellationToken) ?? throw new NotFoundException(typeof(User), request.Login);

        var permissions = await _dbcontext.RolePermissions.Where(p => p.Role == user.Role)
                                          .Select(p => (string)p.Permission.Code)
                                          .ToListAsync(cancellationToken);

        if (user.Password.IsValidPassword(request.Password))
        {
            throw new UnauthorizedAccessException("Неверный пароль");
        }

        // Генерация JWT-токена
        return _jwtProvider.GenerateToken(user, permissions);
    }
}
