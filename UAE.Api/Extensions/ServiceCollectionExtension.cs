using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using UAE.Api.Logging;
using UAE.Api.Middlewares;
using UAE.Api.Validations;

namespace UAE.Api.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddLoggerService(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerManager, LoggerManager>();
    }

    public static void AddCustomExceptionHandler(this IServiceCollection service)
    {
        service.AddScoped<ExceptionMiddleware>();
    }

    public static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddMvcCore();

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ValidateModelStateActionFilter));
            })
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());
    }
}