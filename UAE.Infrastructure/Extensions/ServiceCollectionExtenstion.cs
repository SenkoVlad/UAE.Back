using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using UAE.Core.Entities;
using UAE.Core.Repositories;
using UAE.Core.Repositories.Base;
using UAE.Infrastructure.Repositories;
using UAE.Infrastructure.Repositories.Base;
using UAE.Infrastructure.Repositories.Base.Implementation;
using UAE.Infrastructure.Repositories.Base.Interfaces;

namespace UAE.Infrastructure.Extensions;

public static class ServiceCollectionExtenstion
{
    public static void AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserRepository,UserRepository>();
        serviceCollection.AddScoped<ICategoryRepository, CategoryRepository>();
        serviceCollection.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
        serviceCollection.AddSingleton<ICommandBuilder, CommandBuilder>();
    }
}