using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Domain.ValueObjects.RolePermission;
using Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ali.Delivery.Order.Infrastructure.Persistence.Configurations;

/// <summary>
/// Представляет настройку конфигурации для типа <see cref="RolePermission" />.
/// </summary>
internal class RolePermissionConfiguration : EntityTypeConfigurationBase<RolePermission>
{
       /// <summary>
       /// Вызывается при выполнении конфигурации сущности типа <see cref="RolePermission" />.
       /// </summary>
       /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
       protected override void OnConfigure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("role_permissions", t => t.HasComment("Доступ"));

        builder.HasOne(r => r.Permission)
               .WithMany()
               .HasForeignKey("permission_id");

        builder.Property("permission_id")
               .HasComment("Доступ пользователя");

        builder.Property(r => r.Token)
               .HasMaxLength(RolePermissionToken.TokenLength)
               .HasConversion(p => (string)p!, s => new RolePermissionToken(s))
               .HasComment("Токен");

        builder.HasOne(rp => rp.Role)
               .WithMany(r => r.RolePermissions)
               .HasForeignKey(rp => rp.RoleId)
               .HasConstraintName("role_id")
               .OnDelete(DeleteBehavior.Cascade);
    }
}
