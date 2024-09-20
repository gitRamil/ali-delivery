using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.OrderStatus;
using Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Dictionaries;

/// <summary>
/// Представляет настройку конфигурации для типа <see cref="OrderStatus" />.
/// </summary>
internal class OrderStatusConfiguration : EntityTypeConfigurationBase<OrderStatus>
{
    /// <summary>
    /// Вызывается при выполнении конфигурации сущности типа <see cref="OrderStatus" />.
    /// </summary>
    /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
    protected override void OnConfigure(EntityTypeBuilder<OrderStatus> builder)
    {
        builder.ToTable("order_statuses", t => t.HasComment("Справочник статусов заказов"));

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(OrderStatusName.MaxLength)
               .HasComment("Наименование")
               .HasConversion(o => (string)o, s => new OrderStatusName(s));

        builder.Property(p => p.Code)
               .IsRequired()
               .HasMaxLength(OrderStatusCode.MaxLength)
               .HasComment("Код")
               .HasConversion(o => (string)o, s => new OrderStatusCode(s));

        builder.HasIndex(p => p.Code)
               .IsUnique(); 
    }
}
