using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Shared.ErrorModels;
using System.Net;

namespace Web_Api_E_commerc.Middle_Wares
{
    public class GlobalErrorHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandler> _logger;

        public GlobalErrorHandler(RequestDelegate next, ILogger<GlobalErrorHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
                if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    await HandelNotFoundEndPointAsync(context);
                    var errorResponse = new ErrorDetails
                    {
                        StatusCode = context.Response.StatusCode,
                        ErrorMessage = "Resource not found"
                    };
                    await context.Response.WriteAsync(errorResponse.ToString());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandelNotFoundEndPointAsync(HttpContext context)
        {
            //HttpContext httpContext = context;
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                ErrorMessage = $"The End point {context.Request.Path}not found"
            }.ToString();
            await context.Response.WriteAsync(response);

        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var errorResponse = new ErrorDetails
            {
                ErrorMessage = exception.Message
            };
            context.Response.StatusCode = exception switch
            {
                NotFound_Excption => (int)HttpStatusCode.NotFound,//404
                UnAusthorizedException => (int)HttpStatusCode.Unauthorized,//401
                ValidationExcption validationExcption =>HandelValidationExcption(validationExcption, errorResponse),//400
                _ => (int)HttpStatusCode.InternalServerError//500
            };

            errorResponse.StatusCode = context.Response.StatusCode;

            await context.Response.WriteAsync(errorResponse.ToString());
        }
        private int HandelValidationExcption(ValidationExcption validationExcption, ErrorDetails errorResponse)
        {
            errorResponse.ErrorMessage = validationExcption.Errors.ToString();
            return (int)HttpStatusCode.BadRequest;

        }
    }
}
