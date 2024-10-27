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
               .HasComment("Имя пользователя")
               .HasConversion(f => (string)f, s => new FirstName(s));

        // Настройка свойства LastName
        builder.Property(u => u.LastName)
               .HasMaxLength(LastName.MaxLength)
               .HasComment("Фамилия пользователя")
               .HasConversion(l => (string)l!, s => new LastName(s));

        // Настройка свойства PassportId
        builder.HasOne(p => p.PassportInfo)
               .WithMany()
               .HasForeignKey("passport_info_id");

        builder.Property("passport_info_id")
               .HasComment("Информация о паспорте");

        // Настройка свойства Birthday
        builder.Property(u => u.Birthday)
               .HasComment("Дата рождения пользователя")
               .HasConversion(b => (DateTime)b!, s => new Birthday(s));

        // Настройка свойства RoleId
        builder.HasOne(u => u.Role)
               .WithMany()
               .HasForeignKey("role_id");

        builder.Property("role_id")
               .HasComment("Роль пользователя");
    }
}
