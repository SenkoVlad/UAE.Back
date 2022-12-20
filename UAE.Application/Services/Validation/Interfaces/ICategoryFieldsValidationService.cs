namespace UAE.Application.Services.Validation.Interfaces;

public interface ICategoryFieldsValidationService
{
    bool ValidateByCategory(string[] fields, string categoryId);
}