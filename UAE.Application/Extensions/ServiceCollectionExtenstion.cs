using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using UAE.Application.Services.Implementations;
using UAE.Application.Services.Implementations.User;
using UAE.Application.Services.Interfaces;
using UAE.Application.Services.Interfaces.Base;
using UAE.Application.Services.Interfaces.User;
using UAE.Application.Services.Validation.Implementation;
using UAE.Application.Services.Validation.Interfaces;
using UAE.Application.Validations;
using UAE.Core.DataModels;
using UAE.Core.Entities;

namespace UAE.Application.Extensions;

public static class ServiceCollectionExtenstion
{
    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAnnouncementService, AnnouncementService>();
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IUserBrowsingHistoryService, UserBrowsingHistoryService>();
        serviceCollection.AddScoped<ITokenService, TokenService>();
        serviceCollection.AddScoped<IFileService, FileService>();
        serviceCollection.AddScoped<IPagedQueryBuilderService<Announcement>, PagedQueryBuilderService<Announcement>>();
        
        serviceCollection.AddSingleton<ICategoryFieldsValidationService, CategoryFieldsValidationService>(); 
        serviceCollection.AddSingleton<CategoryFieldsValidationService>();
        
        serviceCollection.AddSingleton<ICategoryInMemory, CategoryInMemory>();
        serviceCollection.AddSingleton<IInMemoryService<Currency>, CurrencyInMemoryService>();
    }
    
    public static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<ApplicationAssembly>(ServiceLifetime.Singleton);
        
        services.AddSingleton<IValidationFactory, ValidationFactory>();
    }
}