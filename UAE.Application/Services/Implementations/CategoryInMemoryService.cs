using UAE.Application.Mapper.Profiles;
using UAE.Application.Models.Category;
using UAE.Application.Services.Interfaces;
using UAE.Application.Services.Interfaces.Base;
using UAE.Core.Entities;
using UAE.Core.Repositories;

namespace UAE.Application.Services.Implementations;

internal sealed class CategoryInMemory : ICategoryInMemory
{
    private readonly ICategoryRepository _categoryRepository;
    public Category[] Data { get; private set; } = Array.Empty<Category>();

    public List<CategoryFlatModel> CategoryFlatModels { get; } = new();
    
    public CategoryPath[] GetCategoryPath(string categoryId)
    {
        var parents = CategoryFlatModels.SingleOrDefault(c => c.Id == categoryId)!
            .ParentCategories;
        
        return parents
            .Select(p => p.ToEntity())
            .ToArray();
    }

    public CategoryInMemory(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task InitAsync()
    {
        await LoadCategoryAsync();
        FillFlatCategoriesFromCategories();
    }

    public bool IsInitialized { get; }

    private async Task LoadCategoryAsync()
    {
        Data = (await _categoryRepository.GetAllAsync())
            .ToArray();
    }

    private void FillFlatCategoriesFromCategories()
    {
        var categories = Data;
        FillFlatCategories(categories, new List<CategoryPathModel>());
    }

    private void FillFlatCategories(Category[] categories, List<CategoryPathModel> parentCategory)
    {
        foreach (var category in categories)
        {
            var newParentCategory = new CategoryPathModel(category.ID, category.Label); 
            
            parentCategory.Add(newParentCategory);

            var newCategory = new CategoryFlatModel
            (
                ParentCategories: parentCategory.ToArray(),
                Fields: category.Fields,
                Id: category.ID,
                Label: category.Label
            );

            CategoryFlatModels.Add(newCategory);
            FillFlatCategories(category.Children.ToArray(), parentCategory);
            parentCategory.Remove(newParentCategory);
        }
    }
}