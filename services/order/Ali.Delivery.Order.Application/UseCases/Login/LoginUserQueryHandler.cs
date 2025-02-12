using Ali.Delivery.Order.Application.Abstractions;
using Ali.Delivery.Order.Application.Exceptions;
using Ali.Delivery.Order.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Order.Application.UseCases.Login;

/// <summary>
/// Представляет обработчик команды для входа пользователя в систему.
/// </summary>
public class LoginQueryHandler : IRequestHandler<LoginUserQuery, string>
{
    private readonly IAppDbContext _dbContext;
    private readonly JwtProvider _jwtProvider;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="LoginQueryHandler" />.
    /// </summary>
    /// <param name="jwtProvider">Провайдер для генерации JWT-токенов.</param>
    /// <param name="dbContext">Контекст взаимодействия с базой данных.</param>
    public LoginQueryHandler(JwtProvider jwtProvider, IAppDbContext dbContext)
    {
        _jwtProvider = jwtProvider;
        _dbContext = dbContext;
    }

    /// <summary>
    /// Обрабатывает запрос на вход пользователя в систему.
    /// </summary>
    /// <param name="query">Запрос, содержащий данные для входа.</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    /// <returns>JWT-токен, если вход выполнен успешно.</returns>
    /// <exception cref="UnauthorizedAccessException">Выбрасывается, если пароль неверен.</exception>
    /// <exception cref="NotFoundException">Выбрасывается, если пользователь не найден.</exception>
    public async Task<string> Handle(LoginUserQuery query, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Login == query.Login, cancellationToken) ?? throw new NotFoundException(typeof(User), query.Login);

        var permissions = await _dbContext.RolePermissions.Where(p => p.Role == user.Role)
                                          .Select(p => (int)p.Permission!.Code)
                                          .ToListAsync(cancellationToken);

        if (user.Password.IsValidPassword(query.Password))
        {
            throw new UnauthorizedAccessException("Неверный пароль");
        }

        return _jwtProvider.GenerateToken(user, permissions);
    }
}
