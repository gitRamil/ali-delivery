namespace Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Base;

/// <summary>
/// Содержит названия базовых полей, общих для всех сущностей.
/// </summary>
internal static class EntityBasePropertyNames
{
    /// <summary>
    /// Название поля, указывающего пользователя, создавшего сущность.
    /// </summary>
    public const string CreatedBy = "CreatedBy";

    /// <summary>
    /// Название поля, указывающего дату создания сущности.
    /// </summary>
    public const string CreatedDate = "CreatedDate";

    /// <summary>
    /// Название поля, указывающего пользователя, обновившего сущность.
    /// </summary>
    public const string UpdatedBy = "UpdatedBy";

    /// <summary>
    /// Название поля, указывающего дату обновления сущности.
    /// </summary>
    public const string UpdatedDate = "UpdatedDate";
}
