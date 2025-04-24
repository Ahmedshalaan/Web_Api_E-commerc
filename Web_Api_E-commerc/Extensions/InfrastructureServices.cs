using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using Presistence.Repositraces;

namespace Web_Api_E_commerc.Extensions
{
    public static class InfrastructureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection Services,IConfiguration configuration)
        { 
            Services.AddScoped<IDbIntializer, DbIntializer>();
            Services.AddScoped<IUnitOfWork, UnitofWork>();

             Services.AddDbContext<ApplicationDbcontext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
         Services.AddCoreService();
           
            return Services;
        }
    }
}
