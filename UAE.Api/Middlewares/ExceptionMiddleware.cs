using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        httpContext.Response.ContentType = "application/json";

        var response = GetResponse(exception);

        await httpContext.Response.WriteAsync(new ErrorDetailsViewModel
        {
            Message = JsonSerializer.Serialize(response),
            StatusCode = httpContext.Response.StatusCode
        }.ToString());
    }

    private static ApiResult<string> GetResponse(Exception exception)
    {
        var errorMessage = $"{exception.GetType()} | {exception.Message}";
        var response = ApiResult<string>.Failure(new[] {errorMessage});
        return response;
    }
}