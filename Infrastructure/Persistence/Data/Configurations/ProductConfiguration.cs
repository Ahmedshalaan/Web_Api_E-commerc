using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Data.Configurations
{
    internal class ProductConfiguration :IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(b => b.ProductBrand)
                .WithMany()
                .HasForeignKey(p => p.BrandId);
 
            builder.HasOne(b => b.ProductType)
                 .WithMany()
                 .HasForeignKey(p => p.TypeId);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);

        }

        
    }
}
