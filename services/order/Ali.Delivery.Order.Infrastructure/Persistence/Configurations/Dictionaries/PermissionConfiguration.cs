using Ali.Delivery.Order.Domain.Entities.Dictionaries;
using Ali.Delivery.Order.Domain.ValueObjects.Dictionaries.Permission;
using Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Dictionaries;

/// <summary>
/// Представляет настройку конфигурации для типа <see cref="Permission" />.
/// </summary>
internal class PermissionConfiguration : EntityTypeConfigurationBase<Permission>
{
    /// <summary>
    /// Вызывается при выполнении конфигурации сущности типа <see cref="Permission" />.
    /// </summary>
    /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
    protected override void OnConfigure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions", t => t.HasComment("Справочник доступа пользователей"));

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(PermissionName.MaxLength)
               .HasConversion(p => (string)p, p => new PermissionName(p))
               .HasComment("Наименование");

        builder.Property(p => p.Code)
               .IsRequired()
               .HasMaxLength(PermissionCode.MaxLength)
               .HasConversion(p => (string)p, p => new PermissionCode(p))
               .HasComment("Код");

        builder.HasIndex(p => p.Code)
               .IsUnique();

        builder.HasData(Permission.GetAllValues());
    }
}
