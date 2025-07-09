using Ali.Delivery.Location.Domain.Entities;
using Ali.Delivery.Location.Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ali.Delivery.Location.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация Entity Framework для сущности <see cref="UserConfig" />.
/// Определяет схему базы данных и связи для конфигураций пользователей.
/// </summary>
internal class UserConfigConfiguration : EntityTypeConfigurationBase<UserConfig>
{
    /// <summary>
    /// Настраивает конфигурацию Entity Framework для сущности <see cref="UserConfig" />.
    /// </summary>
    /// <param name="builder">Построитель конфигурации типа сущности для настройки <see cref="UserConfig" />.</param>
    protected override void OnConfigure(EntityTypeBuilder<UserConfig> builder)
    {
        builder.ToTable("user_config", t => t.HasComment("Конфигурации пользователя"));

        builder.HasOne(u => u.User)
               .WithMany()
               .HasForeignKey("user_id");

        builder.Property(u => u.Language)
               .HasComment("Язык интерфейса пользователя");
    }
}
