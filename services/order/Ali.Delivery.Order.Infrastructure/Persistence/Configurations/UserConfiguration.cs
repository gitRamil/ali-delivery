using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.ValueObjects.User;
using Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ali.Delivery.Order.Infrastructure.Persistence.Configurations;

/// <summary>
/// Представляет настройку конфигурации для типа <see cref="User" />.
/// </summary>
internal class UserConfiguration : EntityTypeConfigurationBase<User>
{
       /// <summary>
       /// Вызывается при выполнении конфигурации сущности типа <see cref="User" />.
       /// </summary>
       /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
       protected override void OnConfigure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users", t => t.HasComment("Пользователь"));

        builder.Property(u => u.UserFirstName)
               .HasMaxLength(UserFirstName.MaxLength)
               .HasConversion(f => (string)f, s => new UserFirstName(s))
               .HasComment("Имя пользователя");

        builder.Property(u => u.UserLastName)
               .HasMaxLength(UserLastName.MaxLength)
               .HasConversion(l => (string)l!, s => new UserLastName(s))
               .HasComment("Фамилия пользователя");

        builder.HasOne(p => p.PassportInfo)
               .WithMany()
               .HasForeignKey("passport_info_id");

        builder.Property("passport_info_id")
               .HasComment("Информация о паспорте");

        builder.Property(u => u.UserBirthDay)
               .HasConversion(b => (DateTime)b!, s => new UserBirthDay(s))
               .HasComment("Дата рождения пользователя");

        builder.HasOne(u => u.Role)
               .WithMany()
               .HasForeignKey("role_id");

        builder.Property("role_id")
               .HasComment("Роль пользователя");
    }
}
