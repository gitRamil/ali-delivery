using Ali.Delivery.Location.Application.Abstractions;

namespace Ali.Delivery.Location.Application.Services;

/// <summary>
/// Представляет сервис дат.
/// </summary>
public class DateTimeService : IDateTimeService
{
    /// <inheritdoc />
    public DateOnly GetCurrentDate()
    {
        return DateOnly.FromDateTime(DateTime.UtcNow);
    }

    /// <inheritdoc />
    public DateTimeOffset GetCurrentDateTime()
    {
        return DateTimeOffset.UtcNow;
    }
}