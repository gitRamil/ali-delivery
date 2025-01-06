namespace Ali.Delivery.Order.Application;

/// <summary>
/// Представляет коды разрешений пользователя для управления доступом к функциям системы.
/// </summary>
public enum UserPermissionCode
{
    /// <summary>
    /// Разрешение на создание нового пользователя.
    /// </summary>
    OrderManagement = 1000,

    /// <summary>
    /// Разрешение на удаление заказа.
    /// </summary>
    UserManagement = 1001,

    /// <summary>
    /// Разрешение на получение информации о заказе.
    /// </summary>
    FullAccess = 1002,

    /// <summary>
    /// Разрешение на обновление информации о заказе.
    /// </summary>
    Tracking = 1003
}
