using Ali.Delivery.Domain.Core;
using Ali.Delivery.Domain.Core.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Base;

/// <summary>
/// Представляет базовую реализацию для настройки конфигурации объектно реляционного отображения для типа <see cref="T" />.
/// </summary>
/// <typeparam name="T">Тип сущности.</typeparam>
/// <seealso cref="IEntityTypeConfiguration{TEntity}" />
internal abstract class EntityTypeConfigurationBase<T> : EntityConfigurationBase<T> where T : Entity<SequentialGuid>
{
    /// <summary>
    /// Вызывается при выполнении конфигурации сущности типа <see cref="T" />.
    /// </summary>
    /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
    protected abstract void OnConfigure(EntityTypeBuilder<T> builder);

    protected override void OnBaseConfigure(EntityTypeBuilder<T> builder)
    {
        builder.Property(p => p.Id)
               .HasConversion(p => (Guid)p, p => p)
               .HasComment("Уникальный идентификатор");
        OnConfigure(builder);
    }
}
