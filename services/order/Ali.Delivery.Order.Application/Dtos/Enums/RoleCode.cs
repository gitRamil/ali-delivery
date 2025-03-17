using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Generators;

namespace Ali.Delivery.Order.Application.Dtos.Enums;

/// <summary>
/// Представляет набор значений, описывающих роли пользователей.
/// </summary>
[MapDictionaryEntity(typeof(Role))]
public enum RoleCode
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    BasicUser,

    /// <summary>
    /// Курьер.
    /// </summary>
    Courier,

    /// <summary>
    /// Неавторизованный пользователь.
    /// </summary>
    NotAuthUser
}
