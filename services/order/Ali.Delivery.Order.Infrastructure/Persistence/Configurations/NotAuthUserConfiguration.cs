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

        builder.Property(u => u.FirstName)
               .HasMaxLength(NotAuthUserFirstName.MaxLength)
               .HasConversion(f => (string?)f, s => new NotAuthUserFirstName(s))
               .HasComment("Имя незарегистрированного пользователя");

        builder.Property(u => u.LastName)
               .HasMaxLength(NotAuthUserLastName.MaxLength)
               .HasConversion(l => (string?)l, s => new NotAuthUserLastName(s))
               .HasComment("Фамилия незарегистрированного пользователя");

        builder.Property(u => u.PhoneNumber)
               .HasMaxLength(NotAuthUserPhoneNumber.MaxLength)
               .HasConversion(p => (string?)p, s => new NotAuthUserPhoneNumber(s))
               .HasComment("Телефонный номер незарегистрированного пользователя");
    }
}
