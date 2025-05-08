
using Domain.Entities.orderEntities;
using Microsoft.AspNetCore.Identity;

namespace Presistence.Data
{
    public class DbIntializer : IDbIntializer
    {
        private readonly ApplicationDbcontext _dbcontext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public DbIntializer(ApplicationDbcontext context, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _dbcontext = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task IntializeAsync()
        {
            try
            {
                //Create database if not exists && Applying Any Pending migration
                if (_dbcontext.Database.GetPendingMigrations().Any())
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

                // ======================== Seed DeliveryMethods ========================


                if (!_dbcontext.DeliveryMethods.Any())
                {
                    // Read data from JSON file
                    var deliveryData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\delivery.json");

                    // Deserialize into C# objects
                    var deliveries = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);

                    // Add to Db & SaveChanges
                    if (deliveries?.Count > 0)
                    {
                        await _dbcontext.DeliveryMethods.AddRangeAsync(deliveries);
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

        public async Task IntializeIdentityAsync()
        {
            // Seed roles
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin")); // تم تصحيح الخطأ الإملائي هنا
            }

            // Seed users - تم نقل هذا خارج شرط ال Roles.Any()
            var adminUser = await _userManager.FindByNameAsync("admin");
            if (adminUser == null)
            {
                adminUser = new User()
                {
                    DisplayName = "Admin",
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    PhoneNumber = "1234567890",
                };
            }

            var superAdminUser = await _userManager.FindByNameAsync("superadmin");
            if (superAdminUser == null)
            {
                superAdminUser = new User()
                {
                    DisplayName = "SuperAdmin",
                    UserName = "superadmin",
                    Email = "superadmin@gmail.com",
                    PhoneNumber = "1234567890",
                };
                await _userManager.CreateAsync(adminUser, "Admin@123");
                await _userManager.AddToRoleAsync(adminUser, "Admin");
                await _userManager.CreateAsync(superAdminUser, "Admin@1234");
                await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
            }
        }
    }
}