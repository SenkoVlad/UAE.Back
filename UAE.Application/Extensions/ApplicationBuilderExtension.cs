using Microsoft.AspNetCore.Builder;
using UAE.Application.Services.Implementations;
using UAE.Application.Services.Interfaces;
using UAE.Application.Services.Interfaces.Base;
using UAE.Core.DataModels;

namespace UAE.Application.Extensions;

public static class ApplicationBuilderExtension
{
    public static async Task InitCategoriesInMemory(this IApplicationBuilder applicationBuilder)
    {
        var categoriesInMemory = (ICategoryInMemory)applicationBuilder.ApplicationServices.GetService(typeof(ICategoryInMemory))!;
        await categoriesInMemory.InitAsync();
    }
    
    public static async Task InitCurrenciesInMemory(this IApplicationBuilder applicationBuilder)
    {
        var currenciesMemory = (IInMemoryService<Currency>)applicationBuilder.ApplicationServices.GetService(typeof(IInMemoryService<Currency>))!;
        await currenciesMemory.InitAsync();
    }
    
    public static async Task InitTempAnnouncements(this IApplicationBuilder applicationBuilder)
    {
        var initTempAnnouncementsService = (InitTempAnnouncementsService)applicationBuilder.ApplicationServices.GetService(typeof(InitTempAnnouncementsService))!;
        await initTempAnnouncementsService.InitAsync();
    }

}