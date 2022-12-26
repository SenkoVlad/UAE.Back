using UAE.Application.Models.Category;
using UAE.Application.Services.Interfaces.Base;
using UAE.Core.Entities;

namespace UAE.Application.Services.Interfaces;

public interface ICategoryInMemory : IInMemoryService<Category>
{
    CategoryPath[] GetCategoryPath(string categoryId);

    List<CategoryFlatModel> CategoryFlatModels { get; }
}