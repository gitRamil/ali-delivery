using Ali.Delivery.Location.Domain.Entities;
using Ali.Delivery.Location.Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ali.Delivery.Location.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация Entity Framework для сущности <see cref="UserConfig" />.
/// </summary>
internal class UserConfigConfiguration : EntityTypeConfigurationBase<UserConfig>
{
       /// <summary>
       /// Настраивает конфигурацию Entity Framework для сущности <see cref="UserConfig" />.
       /// </summary>
       /// <param name="builder">Строитель конфигурации типа сущности для настройки <see cref="UserConfig" />.</param>
       protected override void OnConfigure(EntityTypeBuilder<UserConfig> builder)
    {
        builder.ToTable("user_config", t => t.HasComment("Конфигурации пользователя"));

        builder.HasOne(uc => uc.Language)
               .WithMany()
               .HasForeignKey("language_id");

        builder.Property("language_id")
               .HasComment("Идентификатор словаря языков");

        builder.HasOne(uc => uc.User)
               .WithOne(u => u.UserConfig)
               .HasForeignKey<UserConfig>(uc => uc.Id)
               .IsRequired(false);
    }
}
