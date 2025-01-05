using Ali.Delivery.Order.Domain.Entities.Dictionaries;

namespace Ali.Delivery.Order.Application.Abstractions;

public interface ICurrentUser
{
    /// <summary>Возвращает идентификатор текущего пользователя.</summary>
    /// <returns>
    /// Идентификатор текущего пользователя или <see cref="F:System.Guid.Empty" />, если пользователь является неаутентифицированным.
    /// </returns>
    Guid Id { get; }

    /// <summary>
    /// Возвращает признак, определяющий является ли текущий пользователь аутентифицированным.
    /// </summary>
    /// <returns>
    /// <c>true</c> если пользователь является аутентифицированным; в противном случае, <c>false</c>.
    /// </returns>
    bool IsAuthenticated { get; }

    /// <summary>Определяет содержит ли пользователь разрешение(я).</summary>
    /// <param name="permissions">Разрешения.</param>
    /// <returns>
    /// <c>true</c> если пользователь содержит разрешение(я); в противном случае, <c>false</c>.
    /// </returns>
    bool HasPermission(params UserPermissionCode[] permissions);
}
