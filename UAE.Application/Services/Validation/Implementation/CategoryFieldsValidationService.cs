using UAE.Application.Services.Interfaces;
using UAE.Application.Services.Validation.Interfaces;

namespace UAE.Application.Services.Validation.Implementation;

public class CategoryFieldsValidationService : ICategoryFieldsValidationService
{
    private readonly ICategoryInMemory _categoryInMemory;

    public CategoryFieldsValidationService(ICategoryInMemory categoryInMemory) => 
        _categoryInMemory = categoryInMemory;

    public bool ValidateByCategory(string[] fields, string categoryId)
    {
        var categoryFlatModel = _categoryInMemory.CategoryFlatModels
            .SingleOrDefault(c => c.Id == categoryId && c.Fields
                .Select(a => a.Name)
                .Any(f => fields.Contains(
                    f.ToString(), 
                    StringComparer.OrdinalIgnoreCase)));

        return categoryFlatModel != null;
    }
}