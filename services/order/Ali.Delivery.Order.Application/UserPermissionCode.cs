namespace Ali.Delivery.Order.Application;

/// <summary>
/// Представляет коды разрешений пользователя для управления доступом к функциям системы.
/// </summary>
public enum UserPermissionCode
{
    /// <summary>
    /// Разрешение на создание нового пользователя.
    /// </summary>
    CreateUser = 1000,

    /// <summary>
    /// Разрешение на удаление заказа.
    /// </summary>
    DeleteOrder = 1001,

    /// <summary>
    /// Разрешение на получение информации о заказе.
    /// </summary>
    GetOrder = 1002,

    /// <summary>
    /// Разрешение на обновление информации о заказе.
    /// </summary>
    UpdateOrder = 1003
}
