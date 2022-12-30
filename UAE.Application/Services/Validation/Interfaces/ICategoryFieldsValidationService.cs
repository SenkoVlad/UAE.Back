namespace UAE.Application.Services.Validation.Interfaces;

public interface ICategoryFieldsValidationService
{
    bool DoesFieldExistInAllCategories(string[] fields, string[] categoryIds);
}