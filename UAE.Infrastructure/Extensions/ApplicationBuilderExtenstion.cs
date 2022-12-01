using Microsoft.AspNetCore.Builder;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Options;
using UAE.Infrastructure.Data.Init;
using UAE.Infrastructure.MongoConvention;

namespace UAE.Infrastructure.Extensions;

public static class ApplicationBuilderExtenstion
{
    public static async Task UseMongoDb(this IApplicationBuilder applicationBuilder, string database, string host)
    {
        // ConventionRegistry.Register(
        //     "DictionaryRepresentationConvention",
        //     new ConventionPack {new DictionaryRepresentationConvention(DictionaryRepresentation.ArrayOfArrays)},
        //     _ => true);
        
        await InitDatabase.InitAsync(database, host);
        
    }
}