using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Presentation
{
    internal class RedisCacheAttribute(int durationInSeconds = 120) : ActionFilterAttribute
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.Request.Method != HttpMethods.Get)
            {
                await next();
                return;
            }

            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IService_Manager>().cacheService;
            string cacheKey = GenerateKey(context.HttpContext.Request);

            var cachedValue = await cacheService.GetCachedValueAsync(cacheKey);
            if (cachedValue != null)
            {
                context.Result = new ContentResult
                {
                    Content = cachedValue,
                    ContentType = "application/json",
                    StatusCode = (int)HttpStatusCode.OK
                };
                return;
            }

            var resultContext = await next.Invoke();

            if (resultContext.Result is OkObjectResult objectResult)
            {
                var serialized = JsonSerializer.Serialize(objectResult.Value);
                await cacheService.SetCacheValueAsync(cacheKey, serialized, TimeSpan.FromSeconds(durationInSeconds));
            }
        }

        private string GenerateKey(HttpRequest request)
        {

            var key = new StringBuilder();

            //Request path api/Prodcut
            //Request query ==> Query string 
            //{{baseUrl}}/api/Products?sort=priceAsc&pageSize=S&pageIndex=2
            //{{baseUr1}}/api/Products?pageSize=5&pageIndex=2&sort=priceAsc
            key.Append(request.Path);
            foreach (var query in request.Query.OrderBy(q => q.Key))
            {
                key.Append($"|{query.Key}={query.Value}");
            }
            return key.ToString();
        }
    }
}
