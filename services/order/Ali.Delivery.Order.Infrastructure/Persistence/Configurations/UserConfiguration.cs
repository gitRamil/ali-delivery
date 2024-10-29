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
        // Указываем имя таблицы и добавляем комментарий
        builder.ToTable("users", t => t.HasComment("Пользователь"));

        // Настройка свойства FirstName
        builder.Property(u => u.FirstName)
               .HasMaxLength(FirstName.MaxLength)
               .HasConversion(f => (string)f, s => new FirstName(s))
               .HasComment("Имя пользователя");

        // Настройка свойства LastName
        builder.Property(u => u.LastName)
               .HasMaxLength(LastName.MaxLength)
               .HasConversion(l => (string)l!, s => new LastName(s))
               .HasComment("Фамилия пользователя");

        // Настройка свойства PassportId
        builder.HasOne(p => p.PassportInfo)
               .WithMany()
               .HasForeignKey("passport_info_id");

        builder.Property("passport_info_id")
               .HasComment("Информация о паспорте");

        // Настройка свойства Birthday
        builder.Property(u => u.Birthday)
               .HasConversion(b => (DateTime)b!, s => new Birthday(s))
               .HasComment("Дата рождения пользователя");

        // Настройка свойства RoleId
        builder.HasOne(u => u.Role)
               .WithMany()
               .HasForeignKey("role_id");

        builder.Property("role_id")
               .HasComment("Роль пользователя");
    }
}
