using Web_Api_E_commerc.Extensions;

namespace Web_Api_E_commerc
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
            //Presentation
            builder.Services.AddPresentationServices();

            #region Dependency Injection
            //core

            builder.Services.AddCoreService(builder.Configuration);
            #endregion



        //Infrastructure ==> Presistence ==> call the DbContext ==> call the Repositories==> call the UnitOfWork==> call the Services==> call the Controllers==> call the Endpoints
            builder.Services.AddInfrastructureServices(builder.Configuration);
            #endregion

         var app = builder.Build();

              app.UseCustomExcptionMiddelWare();


            #region Initialize Database
            await app.SeedAcync();
            #endregion

            #region Configure Middlewares

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            #region Static Files Configuration
            app.UseStaticFiles();
            
            #endregion

            app.UseHttpsRedirection();
            app.UseAuthentication();    
            app.UseAuthorization();
            app.MapControllers();
            #endregion

            app.Run();
        }

        
    }
}