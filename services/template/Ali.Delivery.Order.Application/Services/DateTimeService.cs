using Ali.Delivery.Order.Application.Abstractions;

namespace Ali.Delivery.Order.Application.Services;

/// <summary>
/// Представляет сервис дат.
/// </summary>
public class DateTimeService : IDateTimeService
{
    /// <inheritdoc />
    public DateOnly GetCurrentDate() => DateOnly.FromDateTime(DateTime.UtcNow);

    /// <inheritdoc />
    public DateTimeOffset GetCurrentDateTime() => DateTimeOffset.UtcNow;
}
