using System.Diagnostics.CodeAnalysis;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.Permission;


/// <summary>
/// Представляет код разрешения.
/// </summary>
public class PermissionCode : ValueObject
{
    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="PermissionCode" />.
    /// </summary>
    /// <param name="code">Код разрешения.</param>
    /// <exception cref="ArgumentException">
    /// Возникает, если <paramref name="code" /> меньше или равно 0.
    /// </exception>
    public PermissionCode(int code)
    {
        if (code <= 0)
        {
            throw new ArgumentException("Код разрешения не может быть меньше или равно 0", nameof(code));
        }

        _value = code;
    }

    private int _value { get; }

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _value;
    }

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="PermissionCode" /> в <see cref="int" />.
    /// </summary>
    /// <param name="obj">Код разрешения</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator int?(PermissionCode? obj) => obj?._value;

    /// <inheritdoc />
    public override string ToString() => _value.ToString();
}