using Ali.Delivery.Location.Domain.Entities;
using Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ali.Delivery.Location.Infrastructure.Persistence.Configurations;

/// <summary>
/// Представляет настройку конфигурации для типа <see cref="UserLocation" />.
/// </summary>
internal class UserLocationConfiguration : EntityTypeConfigurationBase<UserLocation>
{
       /// <summary>
       /// Вызывается при выполнении конфигурации сущности типа <see cref="UserLocation" />.
       /// </summary>
       /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
       protected override void OnConfigure(EntityTypeBuilder<UserLocation> builder)
    {
        builder.ToTable("userLocations", t => t.HasComment("Локации пользователя"));

        builder.Property(p => p.S)
               .HasComment("Координаты S");

        builder.Property(p => p.E)
               .HasComment("Координаты E");

        builder.Property(p => p.TelegramLogin)
               .HasComment("Ник телеграм");

        builder.HasIndex(u => u.TelegramLogin)
               .IsUnique();
    }
}
