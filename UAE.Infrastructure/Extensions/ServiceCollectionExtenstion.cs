using Microsoft.Extensions.DependencyInjection;
using MongoDB.Entities;
using UAE.Core.Repositories.Base;
using UAE.Infrastructure.Repositories.Base;

namespace UAE.Infrastructure.Extensions;

public static class ServiceCollectionExtenstion
{
    public static void AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IRepositoryBase<Entity>, RepositoryBase>();
    }
}