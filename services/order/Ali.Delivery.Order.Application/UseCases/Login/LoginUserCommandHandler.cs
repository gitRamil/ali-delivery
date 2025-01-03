using Ali.Delivery.Order.Application.Abstractions;
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
        var user = await _dbcontext.Users.FirstOrDefaultAsync(u => u.UserFirstName == request.FirstName, cancellationToken);

        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid login.");
        }

        // Генерация JWT-токена
        return _jwtProvider.GenerateToken(user);
    }
}
