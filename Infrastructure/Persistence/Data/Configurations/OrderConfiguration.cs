using Domain.Entities.orderEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Domain.Entities.orderEntities.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.orderEntities.Order> builder)
        {
            builder.OwnsOne(o => o.shippingAddress, sa =>
                sa.WithOwner());
            builder.HasMany(o => o.OrderItems)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(o => o.paymentStatus).HasConversion(paymentStatus => paymentStatus.ToString(),
                s=>Enum.Parse<OrderPaymentStatus>(s));

            builder.HasOne(o => o.deliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);

            builder.Property(o=>o.SubTotal).HasColumnType("decimal(18,3)").IsRequired();



        }
    }
}
