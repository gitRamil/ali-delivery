using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.ValueObjects.OrderInfo;
using Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ali.Delivery.Order.Infrastructure.Persistence.Configurations;

/// <summary>
/// Представляет настройку конфигурации для типа <see cref="OrderInfo" />.
/// </summary>
internal class OrderInfoConfiguration : EntityTypeConfigurationBase<OrderInfo>
{
       /// <summary>
       /// Вызывается при выполнении конфигурации сущности типа <see cref="OrderInfo" />.
       /// </summary>
       /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
       protected override void OnConfigure(EntityTypeBuilder<OrderInfo> builder)
    {
        builder.ToTable("order_details", t => t.HasComment("Информация о заказах"));

        builder.Property(p => p.Weight)
               .IsRequired()
               .HasConversion(p => (decimal)p, s => new OrderInfoWeight(s))
               .HasComment("Вес заказа");

        builder.Property(p => p.Price)
               .IsRequired()
               .HasConversion(p => (decimal)p, s => new OrderInfoPrice(s))
               .HasComment("Цена заказа");

        builder.Property(p => p.AddressFrom)
               .HasMaxLength(OrderInfoAddressFrom.MaxLength)
               .IsRequired()
               .HasConversion(p => (string)p, s => new OrderInfoAddressFrom(s))
               .HasComment("Адрес отправления");

        builder.Property(p => p.AddressTo)
               .HasMaxLength(OrderInfoAddressTo.MaxLength)
               .IsRequired()
               .HasConversion(p => (string)p, s => new OrderInfoAddressTo(s))
               .HasComment("Адрес доставки");

        builder.HasOne(p => p.Size)
               .WithMany()
               .HasForeignKey("size_id");

        builder.Property("size_id")
               .HasComment("Идентификатор размера");
    }
}
