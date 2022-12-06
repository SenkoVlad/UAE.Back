using FluentValidation;
using FluentValidation.Validators;
using UAE.Core.Entities;

namespace UAE.Api.Validations.CustomValidators;

public class AnnouncementFieldsValidator<T, TProperty> : PropertyValidator<T, TProperty> 
{
    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        var fieldInfos = typeof(Announcement).GetProperties();

        if (value! is Array)
        {
            foreach (var field in (value as Array)!)
            {
                if (fieldInfos.All(fieldInfo => fieldInfo.Name.ToLower() != field!.ToString()?.ToLower()))
                {
                    return false;
                }
            }

            return true;
        }

        return fieldInfos.Any(fieldInfo => fieldInfo.Name.ToLower() == value!.ToString()?.ToLower());
    }
    
    public override string Name => GetType().ToString();

    protected override string GetDefaultMessageTemplate(string errorCode)
        => "Input property must match with entity property";
}