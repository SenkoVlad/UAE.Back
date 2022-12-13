using UAE.Application.Services.Interfaces;

namespace UAE.Application.Validation;

public class CategoryFieldsValidationService
{
    private readonly ICategoryInMemory _categoryInMemory;

    public CategoryFieldsValidationService(ICategoryInMemory categoryInMemory)
    {
        _categoryInMemory = categoryInMemory;
    }

    public bool ValidateByCategory(string[] fields, string categoryId)
    {
        var categoryFlatModel = _categoryInMemory.CategoryFlatModels
            .SingleOrDefault(c => c.Id == categoryId && c.Fields
                .Select(a => a.Name)
                .Any(f => fields.Contains(
                    f.ToString().ToLower(), 
                    StringComparer.OrdinalIgnoreCase)));

        return categoryFlatModel != null;
    }
}