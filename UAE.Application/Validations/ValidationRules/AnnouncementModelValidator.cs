using FluentValidation;
using UAE.Application.Models.Announcement;
using UAE.Application.Validation;

namespace UAE.Application.Validations.ValidationRules;

public class AnnouncementModelValidator : AbstractValidator<AnnouncementModel>
{
    public AnnouncementModelValidator(CategoryFieldsValidationService categoryFieldsValidationService)
    {
        RuleFor(p => new { p.Fields, p.CategoryId} )
            .Must(x => categoryFieldsValidationService.ValidateByCategory(x.Fields.Keys.ToArray(), x.CategoryId));
    }
}