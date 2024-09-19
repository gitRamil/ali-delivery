namespace Ali.Delivery.Order.Domain.Core;

/// <summary>
/// Описывает отслеживание сущности.
/// </summary>
public interface ITrackedEntity
{
    /// <summary>
    /// Возвращает дату и время создания сущности.
    /// </summary>
    DateTimeOffset CreatedDate { get; }

    /// <summary>
    /// Возвращает дату и время обновления сущности.
    /// </summary>
    public DateTimeOffset UpdatedDate { get; }

    /// <summary>
    /// Устанавливает дату и время создания сущности.
    /// </summary>
    void SetCreatedDate();

    /// <summary>
    /// Устанавливает дату и время обновления сущности.
    /// </summary>
    void SetUpdatedDate();
}
