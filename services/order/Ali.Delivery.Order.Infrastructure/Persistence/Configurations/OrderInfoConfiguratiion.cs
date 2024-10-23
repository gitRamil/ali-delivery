using Ali.Delivery.Order.Domain.ValueObjects.OrderInfo;
using Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ali.Delivery.Order.Infrastructure.Persistence.Configurations;

/// <summary>
/// Представляет настройку конфигурации для типа <see cref="Domain.Entities.OrderInfo" />.
/// </summary>
internal class OrderInfoConfiguration : EntityTypeConfigurationBase<Domain.Entities.OrderInfo>
{
    /// <summary>
    /// Вызывается при выполнении конфигурации сущности типа <see cref="Domain.Entities.OrderInfo" />.
    /// </summary>
    /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
    protected override void OnConfigure(EntityTypeBuilder<Domain.Entities.OrderInfo> builder)
    {
        builder.ToTable("order_info", t => t.HasComment("Информация о заказе"));

        builder.Property(p => p.Weight)
            .IsRequired()
            .HasComment("Вес заказа");

        builder.Property(p => p.Price)
            .IsRequired()
            .HasComment("Цена заказа");

        builder.Property(p => p.AddressFrom)
            .HasMaxLength(AddressFrom.MaxLength)
            .IsRequired()
            .HasComment("Адрес отправления");

        builder.Property(p => p.AddressTo)
            .HasMaxLength(AddressTo.MaxLength)
            .IsRequired()
            .HasComment("Адрес доставки");

        builder.HasOne(p => p.Size)
            .WithMany()
            .HasForeignKey("SizeId");

        builder.Property("SizeId")
            .HasComment("Идентификационный номер размера");
    }
}