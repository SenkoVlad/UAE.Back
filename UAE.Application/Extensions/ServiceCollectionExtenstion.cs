using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using UAE.Application.Services.Implementations;
using UAE.Application.Services.Interfaces;
using UAE.Application.Services.Validation.Implementation;
using UAE.Application.Services.Validation.Interfaces;
using UAE.Application.Validations;
using UAE.Core.Entities;

namespace UAE.Application.Extensions;

public static class ServiceCollectionExtenstion
{
    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAnnouncementService, AnnouncementService>();
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<ITokenService, TokenService>();
        serviceCollection.AddScoped<IFileService, FileService>();
        serviceCollection.AddScoped<IPagedQueryBuilderService<Announcement>, PagedQueryBuilderService<Announcement>>();
        
        serviceCollection.AddSingleton<ICategoryFieldsValidationService, CategoryFieldsValidationService>(); 
        serviceCollection.AddSingleton<IFilterFieldsValidationService, FilterFieldsValidationService>(); 
        serviceCollection.AddSingleton<ICategoryInMemory, CategoryInMemory>();
        serviceCollection.AddSingleton<CategoryFieldsValidationService>();
    }
    
    public static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<ApplicationAssembly>(ServiceLifetime.Singleton);
        
        services.AddSingleton<IValidationFactory, ValidationFactory>();
    }
}