using Ali.Delivery.Order.Domain.ValueObjects.Order;
using Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ali.Delivery.Order.Infrastructure.Persistence.Configurations;

/// <summary>
/// Представляет настройку конфигурации для типа <see cref="Domain.Entities.Order" />.
/// </summary>
internal class CourierConfiguration : EntityTypeConfigurationBase<Domain.Entities.Order>
{
    /// <summary>
    /// Вызывается при выполнении конфигурации сущности типа <see cref="Domain.Entities.Order" />.
    /// </summary>
    /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
    protected override void OnConfigure(EntityTypeBuilder<Domain.Entities.Order> builder)
    {
        builder.ToTable("orders", t => t.HasComment("Заказ"));

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(OrderName.MaxLength)
               .HasComment("Наименование заказа")
               .HasConversion(o => (string)o, s => new OrderName(s));
    }
}
