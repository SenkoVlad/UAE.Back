using System.Net;
using UAE.Api.Logging;
using UAE.Api.ViewModels.Base;

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
            await HandleExceptionAsync(httpContext, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        httpContext.Response.ContentType = "application/json";
        var response = GetResponse(exception);
        await httpContext.Response.WriteAsync(response.ToString() ?? string.Empty);
    }

    private static ApiResult<string> GetResponse(Exception exception)
    {
        var errorMessage = $"{exception.GetType()} | {exception.Message}";
        var response = ApiResult<string>.Failure(new[] {errorMessage});
        return response;
    }
}