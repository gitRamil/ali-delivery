namespace Ali.Delivery.Location.Infrastructure.ExternalServices.Models;

public enum BotState
{
    Initial,
    WaitingAuthCommand,
    WaitingCredentials,
    Authenticated,
    GeosharingActive,
    // RegistrationRequired,
    Terminated
}

public enum NotificationType
{
    
    N0_EnterCredentials,      
    N1_Welcome,               
    N1_InvalidAuthCommand,    
    N_SessionEnded,           
    N1_InvalidCommand,        
    N2_InvalidCredentials,
    N3_RegistrationRequired,
    N4_AuthenticationComplete,
    N5_RequestLocation,
    N6_LocationReceived,
    N7_InvalidLocation
}

public record StateTransition(BotState NextState, NotificationType? Notification);

