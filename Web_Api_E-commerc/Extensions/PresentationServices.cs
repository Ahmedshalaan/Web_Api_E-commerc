using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
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
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse;
            });

            Services.ConfigurSwagger(); //✅ بعد نقل الميثود لمكانها الصحيح

            return Services;
        }

        public static IServiceCollection ConfigurSwagger(this IServiceCollection Services)
        {
            Services.AddEndpointsApiExplorer();

            Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid JWT token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
            });

            return Services;
        }
    }
}
