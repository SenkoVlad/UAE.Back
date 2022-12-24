using MongoDB.Driver.Linq;
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
    
    public string[] GetCategoryPath(string categoryId)
    {
        var parents = CategoryFlatModels.SingleOrDefault(c => c.Id == categoryId)!
            .ParentCategories;

        return parents ?? Array.Empty<string>();
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

    private async Task LoadCategoryAsync()
    {
        Categories = await _categoryRepository.GetAllAsync();
    }

    private void FillFlatCategoriesFromCategories()
    {
        var categories = Categories;
        FillFlatCategories(categories, new List<string>());
    }

    private void FillFlatCategories(List<Category> categories, List<string> parentLabels)
    {
        foreach (var category in categories)
        {
            parentLabels.Add(category.Label);

            var newCategory = new CategoryFlatModel
            (
                ParentCategories: parentLabels.ToArray(),
                Fields: category.Fields,
                Id: category.ID,
                Label: category.Label
            );

            CategoryFlatModels.Add(newCategory);
            FillFlatCategories(category.Children, parentLabels);
            parentLabels.Remove(category.Label);
        }
    }
}