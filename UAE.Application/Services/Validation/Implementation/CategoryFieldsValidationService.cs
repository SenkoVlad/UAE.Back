using UAE.Application.Services.Interfaces;
using UAE.Application.Services.Validation.Interfaces;

namespace UAE.Application.Services.Validation.Implementation;

public class CategoryFieldsValidationService : ICategoryFieldsValidationService
{
    private readonly ICategoryInMemory _categoryInMemory;

    public CategoryFieldsValidationService(ICategoryInMemory categoryInMemory) => 
        _categoryInMemory = categoryInMemory;

    public bool DoesFieldExistInAllCategories(string[] fields, string[] categoryIds)
    {
        return  !categoryIds.Any() 
            ? ValidateFieldsAmongAllCategories(fields)
            : ValidateFieldsAmongParticularCategories(fields, categoryIds);
    }

    private bool ValidateFieldsAmongAllCategories(string[] fields)
    {
        var categoryFlatModel = _categoryInMemory.CategoryWithParentPathModels.Select(c => c.Category)
            .FirstOrDefault(c => c.Fields
                .Select(a => a.Name)
                .Any(f => fields.Contains(
                    f.ToString(),
                    StringComparer.OrdinalIgnoreCase)));

        return categoryFlatModel != null;
    }

    private bool ValidateFieldsAmongParticularCategories(string[] fields, string[] categoryIds)
    {
        var categoryFlatModel = _categoryInMemory.CategoryWithParentPathModels.Select(c => c.Category)
            .SingleOrDefault(c => categoryIds.Contains(c.ID) && c.Fields
                .Select(a => a.Name)
                .Any(f => fields.Contains(
                    f.ToString(),
                    StringComparer.OrdinalIgnoreCase)));

        return categoryFlatModel != null;
    }
}