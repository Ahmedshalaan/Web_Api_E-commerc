 
namespace Presistence.Data
{
    public class DbIntializer : IDbIntializer
    {
        private readonly ApplicationDbcontext _dbcontext;

        public DbIntializer(ApplicationDbcontext context)
        {
            _dbcontext = context;
        }

        public async Task IntializeAsync()
        {
            try
            {
                //Create database if not exists && Applying Any Pending migration
                if(_dbcontext.Database.GetPendingMigrations().Any())
                await _dbcontext.Database.MigrateAsync();

                // ======================== Seed ProductTypes ========================

                if (!_dbcontext.ProductTypes.Any())
                {
                    // Read Data from JSON file
                    // E:\Assignments _Route\Api\Web_Api_E-commerc_Solution\Infrastructure\Persistence\Data\Seeding\
                    //  @ => Skip Main Slash
                    var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\types.json");
                    // Transform into C# Objects
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    // Add to Db & SaveChanges
                    if (types?.Count > 0)
                    {
                        await _dbcontext.ProductTypes.AddRangeAsync(types);
                        await _dbcontext.SaveChangesAsync();
                    }
                }

                // ======================== Seed ProductBrands ========================
                if (!_dbcontext.ProductBrands.Any())
                {

                    // Read Data from JSON file
                    //  @ => Skip Main Slash
                    var brandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\brands.json");
                    // Transform into C# Objects 

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    // Add to Db & SaveChanges
                    if (brands?.Count > 0)
                    {
                        await _dbcontext.ProductBrands.AddRangeAsync(brands);
                        await _dbcontext.SaveChangesAsync();
                    }
                }

                // ======================== Seed Products ========================
                if (!_dbcontext.Products.Any())
                {
                    // Read Data from JSON file
                    //  @ => Skip Main Slash
                    var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\products.json");
                    // Transform into C# Objects 
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    // Add to Db & SaveChanges
                    if (products?.Count > 0)
                    {
                        await _dbcontext.Products.AddRangeAsync(products);
                        await _dbcontext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                // Add logging or rethrow
                throw new Exception("An error occurred during DB initialization", ex);
            }
        }
    }
}
