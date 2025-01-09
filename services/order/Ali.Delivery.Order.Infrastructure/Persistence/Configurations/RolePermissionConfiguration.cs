using Ali.Delivery.Order.Domain.Entities;
using Ali.Delivery.Order.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ali.Delivery.Order.Infrastructure.Persistence.Configurations;

internal class RolePermissionConfiguration : EntityTypeConfigurationBase<RolePermission>
{
       protected override void OnConfigure(EntityTypeBuilder<RolePermission> builder)
    {
           builder.ToTable("role_permissions", tableBuilder => tableBuilder.HasComment("Отношение м:м ролей к разрешениям"));

           builder.HasOne(p => p.Role)
                  .WithMany()
                  .HasForeignKey(p => p.RoleId)
                  .OnDelete(DeleteBehavior.Cascade);

           builder.Property(p => p.RoleId)
                  .HasColumnOrder(1)
                  .HasConversion(p => (Guid)p, p => p)
                  .HasComment("Идентификатор роли");

           builder.HasOne(p => p.Permission)
                  .WithMany()
                  .HasForeignKey(p => p.PermissionId)
                  .OnDelete(DeleteBehavior.Cascade);

           builder.Property(p => p.PermissionId)
                  .HasColumnOrder(2)
                  .HasConversion(p => (Guid)p, p => p)
                  .HasComment("Идентификатор разрешения");

           builder.HasKey(i => new
           {
                  i.RoleId,
                  i.PermissionId
           });

           builder.HasData(RolePermission.GetAllValues());
    }
}
