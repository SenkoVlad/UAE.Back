using Microsoft.AspNetCore.Builder;
using UAE.Infrastructure.Data.Init;

namespace UAE.Infrastructure.Extensions;

public static class ApplicationBuilderExtenstion
{
    public static async Task UseMongoDb(this IApplicationBuilder applicationBuilder, string database, string host) => 
        await InitDatabase.InitAsync(database, host);
}