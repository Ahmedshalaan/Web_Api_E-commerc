
using Domain.Entities.orderEntities;

namespace Presistence.Data
{
    public class ApplicationDbcontext : DbContext
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options) : base(options)
        {

        }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure the model using Fluent APIs 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
       public DbSet<Product> Products { get; set; }
       public DbSet<ProductBrand> ProductBrands { get; set; }
       public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Domain.Entities.orderEntities.Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

    }
}
