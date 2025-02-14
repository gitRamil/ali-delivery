using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.ValueObjects.NotAuthUser;
using Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ali.Delivery.Order.Infrastructure.Persistence.Configurations;

/// <summary>
/// Представляет настройку конфигурации для типа <see cref="NotAuthUser" />.
/// </summary>
internal class NotAuthUserConfiguration : EntityTypeConfigurationBase<NotAuthUser>
{
       /// <summary>
       /// Вызывается при выполнении конфигурации сущности типа <see cref="NotAuthUser" />.
       /// </summary>
       /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
       protected override void OnConfigure(EntityTypeBuilder<NotAuthUser> builder)
    {
        builder.ToTable("not_auth_users", t => t.HasComment("Незарегистрированный пользователь"));

        builder.Property(u => u.NotAuthUserFirstName)
               .HasMaxLength(NotAuthUserFirstName.MaxLength)
               .HasConversion(f => (string)f!, s => new NotAuthUserFirstName(s))
               .HasColumnName("first_name")
               .HasComment("Имя незарегистрированного пользователя");

        builder.Property(u => u.NotAuthUserLastName)
               .HasMaxLength(NotAuthUserLastName.MaxLength)
               .HasConversion(l => (string)l!, s => new NotAuthUserLastName(s))
               .HasColumnName("last_name")
               .HasComment("Фамилия незарегистрированного пользователя");

        builder.Property(u => u.NotAuthPhoneNumber)
               .HasMaxLength(NotAuthUserPhoneNumber.MaxLength)
               .HasConversion(p => (string)p!, s => new NotAuthUserPhoneNumber(s))
               .HasColumnName("phone_number")
               .HasComment("Телефонный номер незарегистрированного пользователя");
    }
}
