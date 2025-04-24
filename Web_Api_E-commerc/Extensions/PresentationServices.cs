using Microsoft.AspNetCore.Mvc;
using Web_Api_E_commerc.Factories;

namespace Web_Api_E_commerc.Extensions
{
    public static class PresentationServices
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection Services)
        {
            Services.AddControllers()
                .AddApplicationPart(typeof(Presentation.AssmblyReference_dll).Assembly);
            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse;//Func  <ActionResult, IActionResult> 
            });
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();
            return Services;
        }
    }
}
