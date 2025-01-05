using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.ValueObjects.PassportInfo;
using Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ali.Delivery.Order.Infrastructure.Persistence.Configurations;

/// <summary>
/// Представляет настройку конфигурации для типа <see cref="Domain.Entities.PassportInfo" />.
/// </summary>
internal class PassportInfoConfiguration : EntityTypeConfigurationBase<PassportInfo>
{
       /// <summary>
       /// Вызывается при выполнении конфигурации сущности типа <see cref="Domain.Entities.PassportInfo" />.
       /// </summary>
       /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
       protected override void OnConfigure(EntityTypeBuilder<PassportInfo> builder)
    {
        builder.ToTable("passport", t => t.HasComment("Информация о паспортах"));

        builder.Property(p => p.PassportInfoPassportNumber)
               .IsRequired()
               .HasColumnName("passport_number")
               .HasMaxLength(PassportInfoPassportNumber.MaxLength)
               .HasConversion(b => (string)b, s => new PassportInfoPassportNumber(s))
               .HasComment("Номер паспорта");

        builder.Property(p => p.PassportInfoRegDate)
               .IsRequired()
               .HasColumnName("registration_date")
               .HasConversion(b => (DateTime)b, s => new PassportInfoRegDate(s))
               .HasComment("Дата регистрации");

        builder.Property(p => p.PassportInfoExpirationDate)
               .IsRequired()
               .HasColumnName("expiration_date")
               .HasConversion(b => (DateTime)b, s => new PassportInfoExpirationDate(s))
               .HasComment("Дата истечения срока действия паспорта");

        builder.HasOne(p => p.PassportType)
               .WithMany()
               .HasForeignKey("type_id");

        builder.Property("type_id")
               .HasComment("Идентификационный номер типа паспорта");
    }
}
