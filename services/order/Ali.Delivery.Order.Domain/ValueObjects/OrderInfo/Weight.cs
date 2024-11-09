using System.Diagnostics;
using System.Globalization;
using Ali.Delivery.Domain.Core;

namespace Ali.Delivery.Order.Domain.ValueObjects.OrderInfo;

/// <summary>
/// Представляет вес заказа.
/// </summary>
[DebuggerDisplay("{_weight}")]
public class Weight : ValueObject
{
    private const decimal MaxWeight = 1000m;

    /// <summary>
    /// Минимальный и максимальный вес заказа.
    /// </summary>
    private const decimal MinWeight = 0.1m;

    private readonly decimal _weight;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="Weight" />.
    /// </summary>
    /// <param name="weight">Вес заказа.</param>
    /// <exception cref="ArgumentException">Возникает, если вес меньше минимального или больше максимального.</exception>
    public Weight(decimal weight)
    {
        if (weight < MinWeight || weight > MaxWeight)
        {
            throw new ArgumentException($"Вес заказа должен быть в пределах от {MinWeight} до {MaxWeight} кг.", nameof(weight));
        }

        _weight = weight;
    }

    /// <inheritdoc />
    public override string ToString() => _weight.ToString(CultureInfo.InvariantCulture);

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _weight;
    }

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="Weight" /> в <see cref="decimal" />.
    /// </summary>
    public static implicit operator decimal(Weight obj) => obj._weight;
}
