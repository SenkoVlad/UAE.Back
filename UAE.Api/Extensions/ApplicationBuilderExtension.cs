using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Options;
using UAE.Api.Middlewares;
using UAE.Infrastructure.Data.Init;
using UAE.Infrastructure.MongoConvention;
using UAE.Shared.Settings;

namespace UAE.Api.Extensions;

public static class ApplicationBuilderExtension
{
    public static void UseCustomException(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseMiddleware<ExceptionMiddleware>();
    }
}