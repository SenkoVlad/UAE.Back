using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using UAE.Api.Logging;
using UAE.Api.ViewModels.Base;
using UAE.Application.Models.Base;

namespace UAE.Api.Middlewares;

internal sealed class ExceptionMiddleware  : IMiddleware
{
    private readonly ILoggerManager _loggerManager;

    public ExceptionMiddleware(ILoggerManager loggerManager)
    {
        _loggerManager = loggerManager;
    }

    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception e)
        {
            _loggerManager.LogError($"Something went wrong: {e}");
            await HandleExceptionAsync(httpContext);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext)
    {
        httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        httpContext.Response.ContentType = "application/json";

        var response = GetResponse(httpContext);

        await httpContext.Response.WriteAsync(new ErrorDetailsViewModel
        {
            Message = JsonSerializer.Serialize(response),
            StatusCode = httpContext.Response.StatusCode
        }.ToString());
    }

    private static ApiResult<string> GetResponse(HttpContext httpContext)
    {
        var exception = httpContext.Features.Get<IExceptionHandlerFeature>();

        var errorMessage = exception != null
            ? exception.Error.Message
            : "Internal server error";

        var response = ApiResult<string>.Failure(new[] {errorMessage});
        return response;
    }
}