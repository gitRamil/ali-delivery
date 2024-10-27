using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.Role;
using Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Dictionaries;

/// <summary>
/// Представляет настройку конфигурации для типа <see cref="Role" />.
/// </summary>
internal class RoleConfiguration : EntityTypeConfigurationBase<Role>
{
    /// <summary>
    /// Вызывается при выполнении конфигурации сущности типа <see cref="Role" />.
    /// </summary>
    /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
    protected override void OnConfigure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles", t => t.HasComment("Справочник ролей пользователей"));

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(RoleName.MaxLength)
               .HasConversion(o => (string)o, s => new RoleName(s))
               .HasComment("Наименование");

        builder.Property(p => p.Code)
               .IsRequired()
               .HasMaxLength(RoleCode.MaxLength)
               .HasConversion(o => (string)o, s => new RoleCode(s))
               .HasComment("Код");

        builder.HasIndex(p => p.Code)
               .IsUnique();
    }
}
