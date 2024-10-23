using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.PassportType;
using Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Dictionaries;

/// <summary>
/// Представляет настройку конфигурации для типа <see cref="PassportType" />.
/// </summary>
internal class PassportTypeConfiguration : EntityTypeConfigurationBase<PassportType>
{
    /// <summary>
    /// Вызывается при выполнении конфигурации сущности типа <see cref="PassportType" />.
    /// </summary>
    /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
    protected override void OnConfigure(EntityTypeBuilder<PassportType> builder)
    {
        builder.ToTable("passport_types", t => t.HasComment("Справочник типов паспортов"));

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(PassportTypeName.MaxLength)
            .HasComment("Наименование типа паспорта")
            .HasConversion(o => (string)o, s => new PassportTypeName(s));

        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(PassportTypeCode.MaxLength)
            .HasComment("Код типа паспорта")
            .HasConversion(o => (string)o, s => new PassportTypeCode(s));

        builder.HasIndex(p => p.Code)
            .IsUnique()
            .HasDatabaseName("IX_PassportType_Code");
    }
}