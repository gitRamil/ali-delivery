using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Exceptions;
using Ali.Delivery.Order.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.Login;

/// <summary>
/// Представляет обработчик команды для входа пользователя в систему.
/// </summary>
public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly IAppDbContext _dbcontext;
    private readonly JwtProvider _jwtProvider;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="LoginCommandHandler" />.
    /// </summary>
    /// <param name="jwtProvider">Провайдер для генерации JWT-токенов.</param>
    /// <param name="dbcontext">Контекст взаимодействия с базой данных.</param>
    public LoginCommandHandler(JwtProvider jwtProvider, IAppDbContext dbcontext)
    {
        _jwtProvider = jwtProvider;
        _dbcontext = dbcontext;
    }

    /// <summary>
    /// Обрабатывает запрос на вход пользователя в систему.
    /// </summary>
    /// <param name="request">Запрос, содержащий данные для входа.</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    /// <returns>JWT-токен, если вход выполнен успешно.</returns>
    /// <exception cref="UnauthorizedAccessException">Выбрасывается, если пароль неверен.</exception>
    /// <exception cref="NotFoundException">Выбрасывается, если пользователь не найден.</exception>
    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Login == request.Login, cancellationToken) ?? throw new NotFoundException(typeof(User), request.Login);

        var permissions = await _dbcontext.RolePermissions.Where(p => p.Role == user.Role)
                                          .Select(p => (int)p.Permission!.Code)
                                          .ToListAsync(cancellationToken);

        if (user.Password.IsValidPassword(request.Password))
        {
            throw new UnauthorizedAccessException("Неверный пароль");
        }

        return _jwtProvider.GenerateToken(user, permissions);
    }
}
