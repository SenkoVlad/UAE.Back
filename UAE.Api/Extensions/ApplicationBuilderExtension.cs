using UAE.Api.Middlewares;

namespace UAE.Api.Extensions;

public static class ApplicationBuilderExtension
{
    public static void UseCustomException(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseMiddleware<ExceptionMiddleware>();
    }
}