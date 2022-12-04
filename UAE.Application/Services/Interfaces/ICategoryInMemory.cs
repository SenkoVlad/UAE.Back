using UAE.Application.Models.Category;
using UAE.Core.Entities;

namespace UAE.Application.Services.Interfaces;

public interface ICategoryInMemory
{
    Task InitAsync();
    
    List<Category> Categories { get; }
    
    List<CategoryFlatModel> CategoryFlatModels { get; }
}