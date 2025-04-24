using Services;
using Services.Abstractions;

namespace Web_Api_E_commerc.Extensions
{
    public static class CoreServices
    {
        public static IServiceCollection AddCoreService(this IServiceCollection Services)
        
            {
            //core 
             Services.AddAutoMapper(typeof(Services.AssemplyReference).Assembly);
            Services.AddScoped<IService_Manager, Sevice_Manager>();

            return Services;
        }
    }
}
