using Domain.Contracts;
using Web_Api_E_commerc.Middle_Wares;

namespace Web_Api_E_commerc.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> SeedAcync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbIntializer = scope.ServiceProvider.GetRequiredService<IDbIntializer>();
            await dbIntializer.IntializeAsync();
            return app;
        }
        public static   WebApplication  UseCustomExcptionMiddelWare(this WebApplication app )
        {
            app.UseMiddleware<GlobalErrorHandler>();

            return app;
        }
    }
}
