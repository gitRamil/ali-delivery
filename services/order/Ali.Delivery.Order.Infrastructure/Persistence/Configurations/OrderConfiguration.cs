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
               .HasMaxLength(OrderName.MaxLength)
               .HasConversion(o => (string)o, s => new OrderName(s))
               .HasComment("Наименование заказа");

        builder.HasOne(p => p.OrderStatus)
               .WithMany()
               .HasForeignKey("status_id");

        builder.Property("status_id")
               .HasComment("Статус заказа");

        builder.HasOne(p => p.OrderInfo)
               .WithMany()
               .HasForeignKey("details_id");

        builder.Property("details_id")
               .HasComment("Информация о заказе");

        builder.HasOne(p => p.Sender)
               .WithMany()
               .HasForeignKey("sender_id");

        builder.HasOne(p => p.Receiver)
               .WithMany()
               .HasForeignKey("receiver_id");

        builder.HasOne(p => p.NotAuthReceiver)
               .WithMany()
               .HasForeignKey("not_auth_receiver_id");

        builder.HasOne(p => p.Courier)
               .WithMany()
               .HasForeignKey("courier_id");
    }
}
