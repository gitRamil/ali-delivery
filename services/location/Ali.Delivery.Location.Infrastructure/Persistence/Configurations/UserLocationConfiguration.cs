using Ali.Delivery.Location.Domain.Entities;
using Ali.Delivery.Location.Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ali.Delivery.Location.Infrastructure.Persistence.Configurations;

/// <summary>
/// Представляет настройку конфигурации Entity Framework для типа <see cref="UserLocation" />.
/// Определяет схему базы данных и связи для локаций пользователей.
/// </summary>
internal class UserLocationConfiguration : EntityTypeConfigurationBase<UserLocation>
{
       /// <summary>
       /// Вызывается при выполнении конфигурации сущности типа <see cref="UserLocation" />.
       /// </summary>
       /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
       protected override void OnConfigure(EntityTypeBuilder<UserLocation> builder)
    {
        builder.ToTable("user_locations", t => t.HasComment("Локации пользователя"));

        builder.Property(p => p.S)
               .HasComment("Координаты широты");

        builder.Property(p => p.E)
               .HasComment("Координаты долготы");

        builder.HasOne(u => u.User)
               .WithMany()
               .HasForeignKey("user_id");
    }
}
