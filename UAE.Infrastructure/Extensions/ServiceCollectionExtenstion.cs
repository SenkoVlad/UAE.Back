using Microsoft.Extensions.DependencyInjection;
using UAE.Core.Repositories;
using UAE.Infrastructure.Repositories;

namespace UAE.Infrastructure.Extensions;

public static class ServiceCollectionExtenstion
{
    public static void AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserRepository,UserRepository>();
        serviceCollection.AddScoped<ICategoryRepository, CategoryRepository>();
        serviceCollection.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
    }
}