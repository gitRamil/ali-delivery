using Ali.Delivery.Location.Domain.Entities;
using Ali.Delivery.Location.Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ali.Delivery.Location.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация Entity Framework для сущности <see cref="User" />.
/// Определяет схему базы данных, индексы и ограничения для пользователей.
/// </summary>
internal class UserConfiguration : EntityTypeConfigurationBase<User>
{
       /// <summary>
       /// Настраивает конфигурацию Entity Framework для сущности <see cref="User" />.
       /// </summary>
       /// <param name="builder">Построитель конфигурации типа сущности для настройки <see cref="User" />.</param>
       protected override void OnConfigure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users", t => t.HasComment("Пользователи"));

        builder.Property(u => u.Login)
               .HasComment("Логин пользователя");

        builder.HasIndex(u => u.Login)
               .IsUnique();

        builder.Property(u => u.ChatId)
               .HasComment("ID чата пользователя из телеграма");

        builder.HasIndex(u => u.ChatId)
               .IsUnique();
    }
}
