using System.Linq;
using FluentValidation;
using FluentValidation.Validators;
using UAE.Core.Entities;

namespace UAE.Api.Validations.CustomValidators;

public class SearchAnnouncementsSortedFieldValidator<T, TProperty> : PropertyValidator<T, TProperty> 
{
    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        var fieldInfos = typeof(Announcement).GetProperties();

        return fieldInfos.Any(fieldInfo => fieldInfo.Name == value?.ToString());
    }
    
    public override string Name => GetType().ToString();

    protected override string GetDefaultMessageTemplate(string errorCode)
        => "Sorted property must match with entity property";
}