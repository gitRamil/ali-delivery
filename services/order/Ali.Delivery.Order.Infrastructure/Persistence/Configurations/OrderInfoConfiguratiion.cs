using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.ValueObjects.OrderInfo;
using Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ali.Delivery.Order.Infrastructure.Persistence.Configurations;

/// <summary>
/// Представляет настройку конфигурации для типа <see cref="Domain.Entities.OrderInfo" />.
/// </summary>
internal class OrderInfoConfiguration : EntityTypeConfigurationBase<OrderInfo>
{
    /// <summary>
    /// Вызывается при выполнении конфигурации сущности типа <see cref="Domain.Entities.OrderInfo" />.
    /// </summary>
    /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
    protected override void OnConfigure(EntityTypeBuilder<OrderInfo> builder)
    {
        builder.ToTable("order_info", t => t.HasComment("Информация о заказе"));

        builder.Property(p => p.Weight)
               .IsRequired()
               .HasComment("Вес заказа")
               .HasConversion(p => (decimal)p, s => new Weight(s));

        builder.Property(p => p.Price)
               .IsRequired()
               .HasComment("Цена заказа")
               .HasConversion(p => (decimal)p, s => new Price(s));

        builder.Property(p => p.AddressFrom)
               .HasMaxLength(AddressFrom.MaxLength)
               .IsRequired()
               .HasComment("Адрес отправления")
               .HasConversion(p => (string)p, s => new AddressFrom(s));

        builder.Property(p => p.AddressTo)
               .HasMaxLength(AddressTo.MaxLength)
               .IsRequired()
               .HasComment("Адрес доставки")
               .HasConversion(p => (string)p, s => new AddressTo(s));

        builder.HasOne(p => p.Size)
               .WithMany()
               .HasForeignKey("size_id");

        builder.Property("size_id")
               .HasComment("Идентификационный номер размера");
    }
}
