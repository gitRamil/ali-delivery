using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ali.Delivery.Order.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация для сущности RolePermission.
/// </summary>
internal class RolePermissionConfiguration : EntityTypeConfigurationBase<RolePermission>
{
       /// <summary>
       /// Вызывается при выполнении конфигурации сущности типа <see cref="RolePermission" />.
       /// </summary>
       /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
       protected override void OnConfigure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("role_permissions");

        builder.HasOne(r => r.Role)
               .WithMany()
               .HasForeignKey("role_id"); // Используем свойство RoleId как внешний ключ

        builder.Property("role_id")
               .HasColumnName("role_id")
               .HasComment("Идентификатор роли");

        builder.HasOne(r => r.Permission)
               .WithMany()
               .HasForeignKey("permission_id");

        builder.Property("permission_id")
               .HasComment("Идентификатор разрешения");

        builder.Property(r => r.Token)
               .HasMaxLength(500)
               .HasComment("JWT токен, связанный с разрешением роли");
    }
}
