using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace Web_Api_E_commerc.Factories
{
    public sealed class ApiResponseFactory
    {
        // Context => ModelState => Dictionary <string, ModelEntry>
        // String => Key , Name Of Paramter
        // ModelSateDictionary ===> Objects , Errors
        public static IActionResult CustomValidationErrorResponse(ActionContext actionContext)
        {
            //Get All Errors in modelState 
            var errors = actionContext.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new ValidationError
                {
                    Field = x.Key,
                    Errors = x.Value.Errors
                        .Select(e => e.ErrorMessage)
                        .ToArray()
                }).ToArray();
            //2. Create Custom Response
            var response = new ValidationErrorResponse
            {
                StatuseCode = StatusCodes.Status400BadRequest,
                ErrorMassage = "Validation Error",
                Errors = errors
            };
            return new BadRequestObjectResult(response);
        }

    }
}
