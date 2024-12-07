using Ali.Delivery.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Base;

namespace Ali.Delivery.Order.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация для сущности RolePermission.
/// </summary>
internal class RolePermissionConfiguration : EntityTypeConfigurationBase<RolePermission>
{
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
    
