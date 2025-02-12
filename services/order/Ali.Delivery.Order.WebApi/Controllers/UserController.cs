using Ali.Delivery.Order.Application;
using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Application.UseCases.CompletePassport;
using Ali.Delivery.Order.Application.UseCases.CreateUser;
using Ali.Delivery.Order.Application.UseCases.DeleteUser;
using Ali.Delivery.Order.Application.UseCases.GetAllUsers;
using Ali.Delivery.Order.Application.UseCases.GetCurrentUser;
using Ali.Delivery.Order.Application.UseCases.GetIsUserExist;
using Ali.Delivery.Order.Application.UseCases.GetRolesForUserRegistration;
using Ali.Delivery.Order.Application.UseCases.GetUser;
using Ali.Delivery.Order.Application.UseCases.Login;
using Ali.Delivery.Order.Application.UseCases.UpdateUser;
using Ali.Delivery.Order.WebApi.Attribute;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ali.Delivery.Order.WebApi.Controllers;

/// <summary>
/// Контроллер для управления действиями пользователей.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiExplorerSettings(GroupName = "v1")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="UserController" />.
    /// </summary>
    /// <param name="mediator">Медиатор.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="mediator" /> равен <c>null</c>.
    /// </exception>
    public UserController(IMediator mediator) => _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    /// <summary>
    /// Заполнение данных паспорта пользователя.
    /// </summary>
    /// <param name="command">Команда заполнения данных паспорта пользователя.</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    [HttpPut("complete-passport")]
    [UserPermission(UserPermissionCode.UserManagement)]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CompletePassport([FromBody] CompletePassportCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Создает пользователя.
    /// </summary>
    /// <param name="command">Пользователь.</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    [HttpPost]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Удаляет пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    [HttpDelete]
    [UserPermission(UserPermissionCode.UserManagement)]
    [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(Guid userId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteUserCommand(userId), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получает список всех ролей для пользователей.
    /// </summary>
    /// <param name="cancellationToken">Маркер отмены.</param>
    /// <returns>Список всех ролей.</returns>
    [HttpGet("get-roles")]
    [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetRolesForUserRegistrationQuery(), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получает список всех пользователей.
    /// </summary>
    /// <param name="cancellationToken">Маркер отмены.</param>
    /// <returns>Список всех пользователей.</returns>
    [HttpGet]
    // [UserPermission(UserPermissionCode.UserManagement)]
    [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllUsers(), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получает текущего пользователя.
    /// </summary>
    /// <param name="cancellationToken">Маркер отмены.</param>
    /// <returns>Список всех ролей.</returns>
    [HttpGet("get-current-user")]
    [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCurrentUserQuery(), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Проверяет, существует ли пользователь в системе.
    /// </summary>
    /// <param name="userId">Id пользователя.</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    /// <returns>True если пользователь существует, либо False.</returns>
    [HttpGet("is-user-exist")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetIsUserExist(Guid userId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetIsUserExistQuery(userId), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получает пользователя по идентификатору.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    [HttpGet("{userId}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUser(Guid userId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetUserQuery(userId), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Авторизует пользователя.
    /// </summary>
    /// <param name="userQuery">Логин пользователя.</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    /// <returns>JWT-токен.</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(LoginUserQuery userQuery, CancellationToken cancellationToken)
    {
        var token = await _mediator.Send(userQuery, cancellationToken);

        HttpContext.Response.Cookies.Append("token", token);

        return Ok(token);
    }

    /// <summary>
    /// Выполняет выход пользователя из системы.
    /// </summary>
    /// <returns></returns>
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Logout()
    {
        if (HttpContext.Request.Cookies.ContainsKey("token"))
        {
            HttpContext.Response.Cookies.Delete("token");
        }

        return Ok("Вы успешно вышли из системы.");
    }

    /// <summary>
    /// Обновляет данные пользователя.
    /// </summary>
    /// <param name="command">Команда обновления пользователя.</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    [HttpPut]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}
