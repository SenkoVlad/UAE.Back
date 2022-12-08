using FluentValidation;
using UAE.Application.Models.Announcement;
using UAE.Application.Validation;

namespace UAE.Application.Validations.ValidationRules;

public class PatchAnnouncementModelValidator : AbstractValidator<PatchAnnouncementModel>
{
    public PatchAnnouncementModelValidator(CategoryFieldsValidationService categoryFieldsValidationService)
    {
        When(model => model.Fields != null, () =>
        {
            RuleFor(p => new { p.Fields, p.CategoryId} )
                .Must(x => categoryFieldsValidationService.ValidateByCategory(x.Fields.Keys.ToArray(), x.CategoryId));
        });
    }
}