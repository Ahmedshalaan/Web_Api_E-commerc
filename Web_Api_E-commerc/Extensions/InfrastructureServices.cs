using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Presistence.Data;
using Presistence.Identity;
using Presistence.Repositraces;
using Shared;
using StackExchange.Redis;
using System.Text;

namespace Web_Api_E_commerc.Extensions
{
    public static class InfrastructureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddIdentity<User, IdentityRole>(options =>{ 
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.User.RequireUniqueEmail = true;
            }
            ).AddEntityFrameworkStores<IdentityAppDbContext>();

            Services.AddScoped<IDbIntializer, DbIntializer>();
             Services.AddScoped<IUnitOfWork, UnitofWork>();
            Services.AddScoped<IBasketReposotory, BasketReposotory>();
          

            Services.AddDbContext<ApplicationDbcontext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            Services.AddSingleton<IConnectionMultiplexer>(
                _ => ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!) //Redis connection
                );
            
            Services.AddDbContext<IdentityAppDbContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));
            Services.ConfigureJwt(configuration);
            return Services;
        }
        public static IServiceCollection ConfigureJwt(this IServiceCollection Services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();
            //validate ON Token
                       //يتم تسجيل الدخول 
            Services.AddAuthentication(options =>
            {
              options.DefaultAuthenticateScheme =JwtBearerDefaults.AuthenticationScheme; // لو هو ب الفعل مسجل هرد عليه ازاى 
                options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme; // لو هو مش مسجل و بيحاول يسجل هرد عليه ازاى 

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    //Values
                    ValidAudience= jwtOptions. Audience,
                    ValidIssuer = jwtOptions.Issuer,
                    IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secretkey) )
                };
            });
            //       انت ليك ايه هنا 
            Services.AddAuthorization();
            return Services;
        }
    }
}
