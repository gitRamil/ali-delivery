using Ali.Delivery.Order.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Ali.Delivery.Order.WebApi.Controllers;

/// <summary>
/// Контроллер для работы с полученными локациями.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
[ApiExplorerSettings(GroupName = "v1")]
public class LocationsController : ControllerBase
{
    private readonly ILocationStorageService _locationStorage;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="LocationsController"/>.
    /// </summary>
    /// <param name="locationStorage">Сервис хранения локаций в памяти.</param>
    public LocationsController(
        ILocationStorageService locationStorage)
    {
        _locationStorage = locationStorage;
    }

    /// <summary>
    /// Получить все полученные локации.
    /// </summary>
    /// <returns>Список всех полученных локаций.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllLocations()
    {
        var locations = await _locationStorage.GetAllLocationsAsync();

        var response = new
        {
            TotalCount = locations.Count,
            Locations = locations.Select(l => new
            {
                l.Id,
                l.ChatId,
                l.Longitude,
                l.Latitude,
                l.Timestamp,
                l.ReceivedAt,
                TimeSinceReceived = DateTime.UtcNow - l.ReceivedAt
            })
        };
        return Ok(response);
    }

    /// <summary>
    /// Получить последние N локаций.
    /// </summary>
    /// <param name="count">Количество локаций (максимум 100).</param>
    /// <returns>Список последних локаций.</returns>
    [HttpGet("recent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetRecentLocations([FromQuery] int count = 10)
    {
        if (count <= 0 || count > 100) return BadRequest("Count must be between 1 and 100");

        var locations = await _locationStorage.GetRecentLocationsAsync(count);

        var response = new
        {
            RequestedCount = count,
            ActualCount = locations.Count,
            Locations = locations.Select(l => new
            {
                l.Id,
                l.ChatId,
                l.Longitude,
                l.Latitude,
                l.Timestamp,
                l.ReceivedAt,
                TimeSinceReceived = DateTime.UtcNow - l.ReceivedAt
            })
        };
        return Ok(response);
    }

    /// <summary>
    /// Получить локации по ChatId.
    /// </summary>
    /// <param name="chatId">Идентификатор чата.</param>
    /// <returns>Список локаций для указанного чата.</returns>
    [HttpGet("chat/{chatId:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLocationsByChatId(long chatId)
    {
        var locations = await _locationStorage.GetLocationsByChatIdAsync(chatId);

        var response = new
        {
            ChatId = chatId,
            locations.Count,
            Locations = locations.Select(l => new
            {
                l.Id,
                l.Longitude,
                l.Latitude,
                l.Timestamp,
                l.ReceivedAt,
                TimeSinceReceived = DateTime.UtcNow - l.ReceivedAt
            })
        };

        return Ok(response);
    }

    /// <summary>
    /// Получить статистику по полученным локациям.
    /// </summary>
    /// <returns>Статистика по локациям.</returns>
    [HttpGet("stats")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLocationStats()
    {
        var allLocations = await _locationStorage.GetAllLocationsAsync();

        var stats = new
        {
            TotalLocations = allLocations.Count,
            UniqueChatIds = allLocations.Select(l => l.ChatId).Distinct().Count(),
            LastLocationReceived = allLocations.FirstOrDefault()?.ReceivedAt,
            LocationsInLastHour = allLocations.Count(l => l.ReceivedAt > DateTime.UtcNow.AddHours(-1)),
            LocationsInLastMinute = allLocations.Count(l => l.ReceivedAt > DateTime.UtcNow.AddMinutes(-1)),
            ChatIdStats = allLocations
                .GroupBy(l => l.ChatId)
                .Select(g => new
                {
                    ChatId = g.Key,
                    LocationsCount = g.Count(),
                    LastLocation = g.OrderByDescending(l => l.ReceivedAt).First().ReceivedAt
                })
                .OrderByDescending(s => s.LocationsCount)
                .Take(10)
                .ToList()
        };

        return Ok(stats);
    }

    /// <summary>
    /// Очистить все сохраненные локации (только для Development).
    /// </summary>
    /// <returns>Результат операции.</returns>
    [HttpDelete("clear")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ClearAllLocations()
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (environment != "Development")
            return BadRequest("This endpoint is only available in Development environment");

        await _locationStorage.ClearAllLocationsAsync();

        return Ok(new { Message = "All locations cleared successfully" });
    }
}