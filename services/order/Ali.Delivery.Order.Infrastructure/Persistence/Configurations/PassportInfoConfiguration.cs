using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.ValueObjects.PassportInfo;
using Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ali.Delivery.Order.Infrastructure.Persistence.Configurations;

/// <summary>
/// Представляет настройку конфигурации для типа <see cref="PassportInfo" />.
/// </summary>
internal class PassportInfoConfiguration : EntityTypeConfigurationBase<PassportInfo>
{
    /// <summary>
    /// Вызывается при выполнении конфигурации сущности типа <see cref="PassportInfo" />.
    /// </summary>
    /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
    protected override void OnConfigure(EntityTypeBuilder<PassportInfo> builder)
    {
        builder.ToTable("passport", t => t.HasComment("Информация о паспортах"));

        builder.Property(p => p.PassportNumber)
               .HasMaxLength(PassportInfoPassportNumber.MaxLength)
               .HasConversion(b => (string?)b, s => new PassportInfoPassportNumber(s))
               .HasComment("Номер паспорта");

        builder.Property(p => p.RegDate)
               .HasColumnName("registration_date")
               .HasConversion(b => (DateTime?)b, s => new PassportInfoRegDate(s))
               .HasComment("Дата регистрации");

        builder.Property(p => p.IssuedBy)
               .HasMaxLength(PassportInfoIssuedBy.MaxLength)
               .IsRequired()
               .HasConversion(p => (string?)p, s => new PassportInfoIssuedBy(s))
               .HasComment("Кем выдан");

        builder.HasOne(p => p.PassportType)
               .WithMany()
               .HasForeignKey("type_id");

        builder.Property("type_id")
               .HasComment("Идентификатор типа паспорта");
    }
}
