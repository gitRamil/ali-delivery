namespace Domain.Core;

/// <summary>
/// Представляет ValueObject в терминологии DDD.
/// </summary>
public abstract class ValueObject
{
    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected abstract IEnumerable<object?> GetEqualityComponents();

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (GetType() != obj.GetType())
        {
            return false;
        }

        var valueObject = (ValueObject)obj;

        return GetEqualityComponents()
            .SequenceEqual(valueObject.GetEqualityComponents());
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = new HashCode();

        foreach (var equalityComponent in GetEqualityComponents())
        {
            hashCode.Add(equalityComponent);
        }

        return hashCode.ToHashCode();
    }

    /// <summary>
    /// Реализует оператор ==.
    /// </summary>
    /// <param name="a">Параметр a.</param>
    /// <param name="b">Параметр b.</param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    public static bool operator ==(ValueObject? a, ValueObject? b)
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
    public static bool operator !=(ValueObject? a, ValueObject? b) => !(a == b);
}
