using Microsoft.Extensions.DependencyInjection;
using UAE.Application.Services.Implementations;
using UAE.Application.Services.Interfaces;

namespace UAE.Application.Extensions;

public static class ServiceCollectionExtenstion
{
    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAnnouncementService, AnnouncementService>();
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<ITokenService, TokenService>();
    }
}