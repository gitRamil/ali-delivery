namespace Ali.Delivery.Order.Application.Abstractions;

/// <summary>
/// Описывает сервис дат.
/// </summary>
public interface IDateTimeService
{
    /// <summary>
    /// Возвращает текущую дату в UTC.
    /// </summary>
    DateOnly GetCurrentDate();

    /// <summary>
    /// Возвращает текущую дату и время в UTC.
    /// </summary>
    DateTimeOffset GetCurrentDateTime();
}
