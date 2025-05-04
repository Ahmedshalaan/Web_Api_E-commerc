using Domain.Entities.orderEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(p => p.Price)
       .HasColumnType("decimal(18,3)")
       .IsRequired();
            builder.OwnsOne(o => o.prouductinOrderItem,p=>p.WithOwner()); // say item independ on Product

        }
    }
}
