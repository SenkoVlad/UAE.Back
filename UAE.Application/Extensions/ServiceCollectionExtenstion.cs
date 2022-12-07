using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using UAE.Application.Services.Implementations;
using UAE.Application.Services.Interfaces;
using UAE.Application.Validation;
using UAE.Application.Validations;

namespace UAE.Application.Extensions;

public static class ServiceCollectionExtenstion
{
    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAnnouncementService, AnnouncementService>();
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<ITokenService, TokenService>();
        serviceCollection.AddSingleton<ICategoryInMemory, CategoryInMemory>();
        serviceCollection.AddSingleton<CategoryFieldsValidationService>();
    }
    
    public static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<ApplicationAssembly>(ServiceLifetime.Singleton);
        
        services.AddSingleton<IValidationFactory, ValidationFactory>();
    }
}