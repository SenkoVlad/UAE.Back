using FluentValidation;
using UAE.Application.Models.Announcement;
using UAE.Application.Validation;

namespace UAE.Application.Validations.ValidationRules;

public class UpdateAnnouncementModelValidator : AbstractValidator<UpdateAnnouncementModel>
{
    public UpdateAnnouncementModelValidator(CategoryFieldsValidationService categoryFieldsValidationService)
    {
        RuleFor(p => new { p.Fields, p.CategoryId} )
            .Must(x => categoryFieldsValidationService.ValidateByCategory(x.Fields.Names.ToArray(), x.CategoryId));
    }
}