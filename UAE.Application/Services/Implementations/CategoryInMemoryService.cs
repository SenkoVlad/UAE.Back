using UAE.Application.Mapper.Profiles;
using UAE.Application.Models.Category;
using UAE.Application.Services.Interfaces;
using UAE.Core.Entities;
using UAE.Core.Repositories;

namespace UAE.Application.Services.Implementations;

internal sealed class CategoryInMemory : ICategoryInMemory
{
    private readonly ICategoryRepository _categoryRepository;
    public Category[] Data { get; private set; } = Array.Empty<Category>();

    public List<CategoryWithParentPathModel> CategoryWithParentPathModels { get; } = new();

    public bool IsInitialized { get; private set; }

    public List<string> GetChildrenCategories(List<string> categoryIds)
    {
        var childrenList = new List<string>();

        var children = CategoryWithParentPathModels.Where(c => categoryIds.Contains(c.Category.ID))
            .SelectMany(c => c.ChildrenCategories) 
            .Select(c => c.Id)
            .Distinct()
            .ToList();

        if (children.Any())
        {
            childrenList.AddRange(children);   
            return childrenList;
        }

        return categoryIds;
    }

    public CategoryPath[] GetCategoryPath(string categoryId)
    {
        var parents = CategoryWithParentPathModels.SingleOrDefault(c => c.Category.ID == categoryId)!
            .ParentCategories;
        
        return parents
            .Select(p => p.ToEntity())
            .ToArray();
    }

    public Field? GetField(string categoryId, string fieldName)
    {
        var field = CategoryWithParentPathModels
            .FirstOrDefault(c => c.Category.ID == categoryId
                                                   && c.Category.Fields.Any(f => f.Name == fieldName))
            ?.Fields.FirstOrDefault();

        return field;
    }

    public CategoryInMemory(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task InitAsync()
    {
        await LoadCategoryAsync();
        FillCategoriesFlatWithParentsAndChildren();
        IsInitialized = true;
    }
    
    static IEnumerable<Category> GetTreeNodes(Category rootNode)
    {
        yield return rootNode;
        foreach (var child in rootNode.Children.SelectMany(GetTreeNodes))
        {
            yield return child;
        }
    }
    
    private void FillCategoriesFlatWithParentsAndChildren()
    {
        var categories = Data;
        Fill(categories, new List<CategoryShortModel>());
    }

    private void Fill(Category[] categories, List<CategoryShortModel> parentCategory)
    {
        foreach (var category in categories)
        {
            var newParentCategory = new CategoryShortModel(category.ID, category.Label); 
            
            parentCategory.Add(newParentCategory);

            var children = GetTreeNodes(category)
                .Select(c => new CategoryShortModel(
                    Id: c.ID,
                    Label: c.Label))
                .ToArray();
            
            var newCategory = new CategoryWithParentPathModel(
                ParentCategories: parentCategory.ToArray(),
                ChildrenCategories: children,
                Fields: category.Fields,
                Category: category
            );

            CategoryWithParentPathModels.Add(newCategory);
            Fill(category.Children.ToArray(), parentCategory);
            parentCategory.Remove(newParentCategory);
        }
    }

    private async Task LoadCategoryAsync()
    {
        Data = (await _categoryRepository.GetAllAsync())
            .ToArray();
    }
}