using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.Size;
using Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Dictionaries;

/// <summary>
/// Представляет настройку конфигурации для типа <see cref="Size" />.
/// </summary>
internal class SizeConfiguration : EntityTypeConfigurationBase<Size>
{
    /// <summary>
    /// Вызывается при выполнении конфигурации сущности типа <see cref="Size" />.
    /// </summary>
    /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
    protected override void OnConfigure(EntityTypeBuilder<Size> builder)
    {
        builder.ToTable("sizes", t => t.HasComment("Справочник размеров"));

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(SizeName.MaxLength)
               .HasConversion(o => (string)o, s => new SizeName(s))
               .HasComment("Наименование");

        builder.Property(p => p.Code)
               .IsRequired()
               .HasMaxLength(SizeCode.MaxLength)
               .HasConversion(o => (string)o, s => new SizeCode(s))
               .HasComment("Код");

        builder.HasIndex(p => p.Code)
               .IsUnique();

        builder.HasData(Size.GetAllValues());
    }
}
