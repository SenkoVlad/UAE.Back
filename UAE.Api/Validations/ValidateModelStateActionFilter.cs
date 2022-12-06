using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UAE.Api.ViewModels.Base;

namespace UAE.Api.Validations;

public class ValidateModelStateActionFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values.Where(v => v.Errors.Any())
                .SelectMany(v => v.Errors)
                .Select(v => v.ErrorMessage)
                .ToArray();

            var response = ApiResult<string>.Failure(errors);

            context.Result = new JsonResult(JsonSerializer.Serialize(response))
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }
    }
}