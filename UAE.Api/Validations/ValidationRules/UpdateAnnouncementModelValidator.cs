using FluentValidation;
using UAE.Api.Validations.CustomValidators;
using UAE.Application.Models.Announcement;

namespace UAE.Api.Validations.ValidationRules;

public class UpdateAnnouncementModelValidator : AbstractValidator<UpdateAnnouncementModel>
{
    public UpdateAnnouncementModelValidator()
    {
        RuleFor(p => p.FieldsValuesToUpdate.Keys.ToArray())
            .SetValidator(new AnnouncementFieldsValidator<UpdateAnnouncementModel, string[]>());
    }
}