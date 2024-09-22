using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.Roles;
using Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Dictionaries;

/// <summary>
/// Представляет настройку конфигурации для типа <see cref="Roles" />.
/// </summary>
internal class RolesConfiguration : EntityTypeConfigurationBase<Roles>
{
    /// <summary>
    /// Вызывается при выполнении конфигурации сущности типа <see cref="Roles" />.
    /// </summary>
    /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
    protected override void OnConfigure(EntityTypeBuilder<Roles> builder)
    {
        builder.ToTable("roles", t => t.HasComment("Справочник ролей пользователей"));

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(RolesName.MaxLength)
            .HasComment("Наименование")
            .HasConversion(o => (string)o, s => new RolesName(s));

        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(RolesCode.MaxLength)
            .HasComment("Код")
            .HasConversion(o => (string)o, s => new RolesCode(s));

        builder.HasIndex(p => p.Code)
            .IsUnique();
    }
}
