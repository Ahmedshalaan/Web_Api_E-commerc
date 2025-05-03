using Services;
using Services.Abstractions;
using Shared;

namespace Web_Api_E_commerc.Extensions
{
    public static class CoreServices
    {
        public static IServiceCollection AddCoreService(this IServiceCollection Services,IConfiguration configuration)
        
            {
            //core 
             Services.AddAutoMapper(typeof(Services.AssemplyReference).Assembly);
            Services.AddScoped<IService_Manager, Sevice_Manager>();
            Services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
            return Services;
        }
    }
}
