using System.Reflection;
using FluentValidation;
using FluentValidation.Validators;
using UAE.Application.Services.Validation.Interfaces;
using UAE.Application.Validations.ValidationParameters;
using UAE.Core.Entities;

namespace UAE.Application.Validations.CustomValidators;

public class AnnouncementFieldsValidator<T, TProperty> : PropertyValidator<T, TProperty> 
{
    private readonly ICategoryFieldsValidationService _categoryFieldsValidationService;
    
    public AnnouncementFieldsValidator(ICategoryFieldsValidationService categoryFieldsValidationService)
    {
        _categoryFieldsValidationService = categoryFieldsValidationService;
    }

    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        var propertyInfos = typeof(Announcement).GetProperties();

        if (value is not AnnouncementFieldParameter sortByParameter)
        {
            return false;
        }
        
        return DoesSortFieldExistInAnnouncement(propertyInfos, sortByParameter) || 
               DoesSortFieldExistInExtraAnnouncementFields(sortByParameter);
    }

    private bool DoesSortFieldExistInExtraAnnouncementFields(AnnouncementFieldParameter field)
    {
        var extraField = IsItExtraField(field.FieldName);

        return !string.IsNullOrWhiteSpace(extraField) &&
               _categoryFieldsValidationService.DoesFieldExistInAllCategories(new[] {extraField}, field.CategoryIds);
    }

    private string IsItExtraField(string sortedBy)
    {
        //Fields.Floor
        var doesItContainTwoWords = sortedBy.Split('.').Length == 2;

        if (doesItContainTwoWords)
        {
            return sortedBy.Split('.').Last();
        }

        return string.Empty;
    }

    private static bool DoesSortFieldExistInAnnouncement(PropertyInfo[] extraFields, AnnouncementFieldParameter field) => 
        extraFields.Any(fieldInfo => String.Equals(fieldInfo.Name, field.FieldName, StringComparison.CurrentCultureIgnoreCase));

    public override string Name =>
        GetType().ToString();
}