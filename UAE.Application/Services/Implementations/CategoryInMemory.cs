using UAE.Application.Models.Category;
using UAE.Application.Services.Interfaces;
using UAE.Core.Entities;
using UAE.Core.Repositories;

namespace UAE.Application.Services.Implementations;

class CategoryInMemory : ICategoryInMemory
{
    private readonly ICategoryRepository _categoryRepository;
    public List<Category> Categories { get; private set; } = new();

    public List<CategoryFlatModel> CategoryFlatModels { get; } = new();

    public CategoryInMemory(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task InitAsync()
    {
        await LoadCategoryAsync();
        FillFlatCategoriesFromCategories();
    }

    private async Task LoadCategoryAsync()
    {
        Categories = await _categoryRepository.GetAllAsync();
    }

    private void FillFlatCategoriesFromCategories()
    {
        var categories = Categories;
        FillFlatCategories(categories);
    }

    private void FillFlatCategories(List<Category> categories)
    {
        foreach (var category in categories)
        {
            CategoryFlatModels.Add(new CategoryFlatModel
            (
                Fields: category.Fields,
                Id: category.ID,
                Label: category.Label
            ));

            FillFlatCategories(category.Children);
        }
    }
}