namespace Ali.Delivery.Location.Application.Models;

/// <summary>
/// Представляет географические координаты.
/// </summary>
/// <param name="Latitude">Координата широты.</param>
/// <param name="Longitude">Координата долготы.</param>
public record MessageLocation(double Latitude, double Longitude);
