using Ali.Delivery.Location.Domain.Entities.Dictionaries;
using Ali.Delivery.Location.Domain.ValueObjects.Dictionaries.LanguageDictionary;
using Ali.Delivery.Location.Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ali.Delivery.Location.Infrastructure.Persistence.Configurations.Dictionaries;

/// <summary>
/// Конфигурация Entity Framework для сущности <see cref="LanguageDictionary" />.
/// </summary>
internal class LanguageDictionaryConfiguration : EntityTypeConfigurationBase<LanguageDictionary>
{
    /// <summary>
    /// Настраивает конфигурацию Entity Framework для сущности<see cref="LanguageDictionary" />.
    /// </summary>
    /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
    protected override void OnConfigure(EntityTypeBuilder<LanguageDictionary> builder)
    {
        builder.ToTable("language_dictionary", t => t.HasComment("Справочник языков"));

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(LanguageName.MaxLength)
               .HasConversion(o => (string)o, s => new LanguageName(s))
               .HasComment("Наименование");

        builder.Property(p => p.Code)
               .IsRequired()
               .HasMaxLength(LanguageCode.MaxLength)
               .HasConversion(o => (string)o, s => new LanguageCode(s))
               .HasComment("Код");

        builder.HasIndex(p => p.Code)
               .IsUnique();

        builder.HasData(LanguageDictionary.GetAllValues());
    }
}
