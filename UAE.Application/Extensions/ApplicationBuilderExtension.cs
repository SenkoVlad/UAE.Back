using Microsoft.AspNetCore.Builder;
using UAE.Application.Services.Interfaces;

namespace UAE.Application.Extensions;

public static class ApplicationBuilderExtension
{
    public static async Task InitCategoriesInMemory(this IApplicationBuilder applicationBuilder)
    {
        var categoriesInMemory = (ICategoryInMemory)applicationBuilder.ApplicationServices.GetService(typeof(ICategoryInMemory))!;
        await categoriesInMemory.InitAsync();
    }
}