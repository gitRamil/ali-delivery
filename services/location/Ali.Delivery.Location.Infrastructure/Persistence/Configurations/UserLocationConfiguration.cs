using Ali.Delivery.Location.Domain.Entities;
using Ali.Delivery.Location.Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ali.Delivery.Location.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация Entity Framework для сущности <see cref="UserLocation" />.
/// </summary>
internal class UserLocationConfiguration : EntityTypeConfigurationBase<UserLocation>
{
       /// <summary>
       /// Настраивает конфигурацию Entity Framework для сущности <see cref="UserLocation" />.
       /// </summary>
       /// <param name="builder">Строитель конфигурации типа сущности для настройки <see cref="UserLocation" />.</param>
       protected override void OnConfigure(EntityTypeBuilder<UserLocation> builder)
    {
        builder.ToTable("user_locations", t => t.HasComment("Локации пользователя"));

        builder.Property(p => p.Latitude)
               .HasComment("Координаты широты");

        builder.Property(p => p.Longitude)
               .HasComment("Координаты долготы");

        builder.HasOne(u => u.User)
               .WithMany(ul => ul.UserLocations)
               .HasForeignKey("user_id");

        builder.Property("user_id")
               .HasComment("Идентификатор пользователя");
    }
}
