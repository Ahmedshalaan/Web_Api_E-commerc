using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using Presistence.Repositraces;
using StackExchange.Redis;

namespace Web_Api_E_commerc.Extensions
{
    public static class InfrastructureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection Services,IConfiguration configuration)
        { 
            Services.AddScoped<IDbIntializer, DbIntializer>();
            Services.AddScoped<IUnitOfWork, UnitofWork>();
            Services.AddScoped<IBasketReposotory, BasketReposotory>();

             Services.AddDbContext<ApplicationDbcontext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
         Services.AddCoreService();
           Services.AddSingleton<IConnectionMultiplexer>(
               _ =>ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!) //Redis connection
               );
            return Services;
        }
    }
}
