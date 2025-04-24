
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

    }
}
