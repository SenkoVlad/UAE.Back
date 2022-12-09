using System.Reflection;
using FluentValidation;
using FluentValidation.Validators;
using UAE.Application.Validation;
using UAE.Application.Validations.ValidationParameters;
using UAE.Core.Entities;

namespace UAE.Application.Validations.CustomValidators;

public class AnnouncementFieldsValidator<T, TProperty> : PropertyValidator<T, TProperty> 
{
    private readonly CategoryFieldsValidationService _categoryFieldsValidationService;

    public AnnouncementFieldsValidator(CategoryFieldsValidationService categoryFieldsValidationService)
    {
        _categoryFieldsValidationService = categoryFieldsValidationService;
    }

    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        var propertyInfos = typeof(Announcement).GetProperties();

        var sortByParameter = value as AnnouncementSortByParameter;

        if (sortByParameter == null)
        {
            return false;
        }
        
        return DoesSortFieldExistInAnnouncement(propertyInfos, sortByParameter) || 
               DoesSortFieldExistInExtraAnnouncementFields(sortByParameter);
    }

    private bool DoesSortFieldExistInExtraAnnouncementFields(AnnouncementSortByParameter field) => 
        _categoryFieldsValidationService.ValidateByCategory(new[] {field.SortedBy}, field.CategoryId);

    private static bool DoesSortFieldExistInAnnouncement(PropertyInfo[] extraFields, AnnouncementSortByParameter field) => 
        extraFields.Any(fieldInfo => String.Equals(fieldInfo.Name, field.SortedBy, StringComparison.CurrentCultureIgnoreCase));

    public override string Name =>
        GetType().ToString();

    protected override string GetDefaultMessageTemplate(string errorCode) =>
        "Input property must match with entity property";
}