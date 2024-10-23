using Ali.Delivery.Order.Domain.ValueObjects.PassportInfo;
using Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ali.Delivery.Order.Infrastructure.Persistence.Configurations;

/// <summary>
/// Представляет настройку конфигурации для типа <see cref="Domain.Entities.PassportInfo" />.
/// </summary>
internal class PassportInfoConfiguration : EntityTypeConfigurationBase<Domain.Entities.PassportInfo>
{
    /// <summary>
    /// Вызывается при выполнении конфигурации сущности типа <see cref="Domain.Entities.PassportInfo" />.
    /// </summary>
    /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
    protected override void OnConfigure(EntityTypeBuilder<Domain.Entities.PassportInfo> builder)
    {
        builder.ToTable("passport_info", t => t.HasComment("Информация о паспортах"));

        builder.Property(p => p.PassportNumber)
            .IsRequired()
            .HasMaxLength(PassportNumber.MaxLength)
            .HasComment("Номер паспорта");

        builder.Property(p => p.RegDate)
            .IsRequired()
            .HasComment("Дата регистрации");

        builder.Property(p => p.ExpirationDate)
            .IsRequired()
            .HasComment("Дата истечения срока действия паспорта");

        builder.HasOne(p => p.TypeId)
            .WithMany()
            .HasForeignKey("TypeId");

        builder.Property("TypeId")
            .HasComment("Идентификационный номер типа паспорта");


    }
}