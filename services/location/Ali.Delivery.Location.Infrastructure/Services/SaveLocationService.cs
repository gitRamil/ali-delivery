using Ali.Delivery.Location.Application.Abstractions;
using Ali.Delivery.Location.Application.UseCases.CreateUserLocation;
using Ali.Delivery.Location.Application.UseCases.UpdateUserLocation;
using Ali.Delivery.Location.Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ali.Delivery.Location.Infrastructure.Services;

/// <summary>
/// Сервис для управления логикой создания или обновления местоположения пользователя.
/// </summary>
public class SaveLocationService
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="SaveLocationService" />.
    /// </summary>
    /// <param name="mediator">Экземпляр MediatR для отправки команд.</param>
    /// <param name="context">Контекст базы данных для проверки существования пользователя.</param>
    public SaveLocationService(IMediator mediator, IAppDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    /// <summary>
    /// Создает нового пользователя с локацией или обновляет локацию существующего.
    /// Эта функция принимает данные, которые вы получаете от Telegram-бота.
    /// </summary>
    /// <param name="userLogin">Логин пользователя в Telegram.</param>
    /// <param name="e">Координата E (например, долгота).</param>
    /// <param name="s">Координата S (например, широта).</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    /// <returns>Строка с результатом выполнения команды.</returns>
    public async Task<CommandResult> CreateOrUpdateUserLocationAsync(string userLogin, string e, string s, CancellationToken cancellationToken = default)
    {
        var userExists = await _context.UserLocations.AnyAsync(u => u.TelegramLogin == userLogin, cancellationToken);

        if (userExists)
        {
            var updateCommand = new UpdateUserLocationCommand(userLogin, e, s);
            await _mediator.Send(updateCommand, cancellationToken);
            return new CommandResult(string.Empty);
        }

        var createCommand = new CreateUserLocationCommand(userLogin, e, s);

        await _mediator.Send(createCommand, cancellationToken);
        return new CommandResult(string.Empty);
    }
}
