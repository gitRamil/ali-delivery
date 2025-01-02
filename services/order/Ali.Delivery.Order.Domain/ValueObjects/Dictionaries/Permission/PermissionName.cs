using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.Permission;

/// <summary>
/// Представляет наименование справочника типов доступа пользователей.
/// </summary>
[DebuggerDisplay("{_name}")]
public sealed class PermissionName : ValueObject
{
    /// <summary>
    /// Представляет максимальную длину наименования справочника типов доступа.
    /// </summary>
    public const int MaxLength = 250;

    private readonly string _name;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="PermissionName" />.
    /// </summary>
    /// <param name="name">Наименование справочника типов доступа.</param>
    /// <exception cref="ArgumentException">
    /// Возникает, если <paramref name="name" />
    /// является <c>null</c> или <c>whitespace</c> или его длина превышает <see cref="MaxLength" />.
    /// </exception>
    public PermissionName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Наименование справочника типов доступа не может быть null или пустой строкой.", nameof(name));
        }

        if (name.Length > MaxLength)
        {
            throw new ArgumentException($"Наименование справочника типов доступа не может быть больше {MaxLength}.", nameof(name));
        }

        _name = name;
    }

    /// <inheritdoc />
    public override string ToString() => _name;

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _name;
    }

    /// <summary>
    /// Выполняет явное преобразование из <see cref="string" /> в <see cref="PermissionCode" />.
    /// </summary>
    /// <param name="obj">Описание цели.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static explicit operator PermissionName?(string? obj) => obj == null ? null : new PermissionName(obj);

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="PermissionCode" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Значение наименования типов доступа.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(PermissionName? obj) => obj?._name;
}
