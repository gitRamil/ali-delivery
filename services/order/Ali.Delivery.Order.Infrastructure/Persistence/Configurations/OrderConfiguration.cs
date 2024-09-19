using Ali.Delivery.Domain.Core.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ali.Delivery.Order.Infrastructure.Persistence.Configurations;

internal class OrderConfiguration : IEntityTypeConfiguration<Domain.Entities.Order>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Order> builder)
    {
        builder.ToTable("orders", t => t.HasComment("Заказ"));
        
        builder.Property(p => p.Id)
               .HasConversion(p => (Guid)p, p => (SequentialGuid)p);
        builder.HasKey(p => p.Id); 
        
        builder.Property(p => p.Test)
               .HasComment("Test");
    }
}
