using Ali.Delivery.Order.Application.UseCases.GetAllUsers;
using Microsoft.AspNetCore.Mvc;
using Ali.Delivery.Order.Application.Dtos.Order;
using Ali.Delivery.Order.Application.UseCases.CreateUser;
using Ali.Delivery.Order.Application.UseCases.DeleteUser;
using Ali.Delivery.Order.Application.UseCases.GetUser;
using MediatR;

namespace Ali.Delivery.Order.WebApi.Controllers
{
    /// <summary>
    /// Контроллер для управления действиями пользователей.
    /// </summary>
    /// <remarks>
    /// Этот контроллер предоставляет API для выполнения операций, связанных с пользователями,
    /// таких как получение, создание, обновление и удаление.
    /// </remarks>
    /// <response code="200">Запрос выполнен успешно.</response>
    /// <response code="400">Ошибочный запрос.</response>
    /// <response code="404">Ресурс не найден.</response>
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
        /// Создает пользователя.
        /// </summary>
        /// <param name="command">Пользователь.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        [HttpPost]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Получает пользователя по идентификатору.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        [HttpGet("{userId:guid}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(Guid userId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetUserCommand(userId), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Получает список всех пользователей.
        /// </summary>
        /// <param name="cancellationToken">Маркер отмены.</param>
        /// <returns>Список всех пользователей.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllUsers(), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Удаляет пользователя.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken">Маркер отмены.</param>
        [HttpDelete]
        [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(Guid userId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteUserCommand(userId), cancellationToken);
            return Ok(result);
        }
    }
}
