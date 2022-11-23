using Microsoft.Extensions.DependencyInjection;
using UAE.Core.Repositories;
using UAE.Infrastructure.Repositories;

namespace UAE.Infrastructure.Extensions;

public static class ServiceCollectionExtenstion
{
    public static void AddRepositories(this IServiceCollection serviceCollection)
    {
        // serviceCollection.AddScoped<IRepositoryBase<User>, RepositoryBase<User>>();
        serviceCollection.AddScoped<IUserRepository,UserRepository>();
    }
}