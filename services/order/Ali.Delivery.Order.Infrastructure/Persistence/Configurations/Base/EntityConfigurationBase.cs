using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Base;

/// <summary>
/// Представляет базовую реализацию для настройки конфигурации объектно-реляционного отображения для заданного типа
/// сущности.
/// </summary>
/// <typeparam name="T">Тип сущности.</typeparam>
internal abstract class EntityConfigurationBase<T> : IEntityTypeConfiguration<T> where T : class
{
       /// <summary>
       /// Вызывается при выполнении конфигурации базовой сущности для заданного типа.
       /// </summary>
       /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
       protected abstract void OnBaseConfigure(EntityTypeBuilder<T> builder);

    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property<DateTimeOffset>(EntityBasePropertyNames.CreatedDate)
               .HasDefaultValue(new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero));

        builder.Property<DateTimeOffset>(EntityBasePropertyNames.UpdatedDate)
               .HasDefaultValue(new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero));

        builder.Property<string?>(EntityBasePropertyNames.CreatedBy)
               .HasMaxLength(100);

        builder.Property<string?>(EntityBasePropertyNames.UpdatedBy)
               .HasMaxLength(100);

        OnBaseConfigure(builder);
    }
}
