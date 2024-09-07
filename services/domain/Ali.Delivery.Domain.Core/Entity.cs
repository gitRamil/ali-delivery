using System.Diagnostics.CodeAnalysis;

namespace Ali.Delivery.Domain.Core;

/// <summary>
/// Представляет Entity в терминологии DDD.
/// </summary>
/// <typeparam name="TId">Тип идентификатора.</typeparam>
public abstract class Entity<TId>
{
    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="Entity{TId}" />.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="id" /> равен <c>null</c>.
    /// </exception>
    protected Entity([DisallowNull] TId id) => Id = id ?? throw new ArgumentNullException(nameof(id));

    /// <summary>
    /// Возвращает идентификатор.
    /// </summary>
    /// <value>
    /// Идентификатор.
    /// </value>
    public TId Id { get; }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TId> other)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (GetRealType() != other.GetRealType())
        {
            return false;
        }

        if (IsTransient() || other.IsTransient())
        {
            return false;
        }

        return Id!.Equals(other.Id);
    }

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(Id);

    private Type? GetRealType()
    {
        var type = GetType();

        if (type.ToString()
                .Contains("Castle.Proxies."))
        {
            return type.BaseType;
        }

        return type;
    }

    private bool IsTransient() => Id is null || Id.Equals(default(TId));

    /// <summary>
    /// Реализует оператор ==.
    /// </summary>
    /// <param name="a">Параметр a.</param>
    /// <param name="b">Параметр b.</param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    public static bool operator ==(Entity<TId>? a, Entity<TId>? b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    /// <summary>
    /// Реализует оператор !=.
    /// </summary>
    /// <param name="a">Параметр a.</param>
    /// <param name="b">Параметр b.</param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    public static bool operator !=(Entity<TId>? a, Entity<TId>? b) => !(a == b);
}
