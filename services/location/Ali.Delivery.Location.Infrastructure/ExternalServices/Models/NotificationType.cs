namespace Ali.Delivery.Location.Infrastructure.ExternalServices.Models;

public enum NotificationType
{
    N0_EnterCredentials,
    N1_Welcome,
    N1_InvalidAuthCommand,
    N_SessionEnded,
    N1_InvalidCommand,
    N2_InvalidCredentials,
    N4_AuthenticationComplete,
    N5_RequestLocation,
    N6_LocationReceived,
    N7_InvalidLocation
}
