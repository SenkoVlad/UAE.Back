using FluentValidation;
using UAE.Application.Models.Announcement;
using UAE.Application.Services.Validation.Implementation;

namespace UAE.Application.Validations.ValidationRules;

public class PatchAnnouncementModelValidator : AbstractValidator<PatchAnnouncementModel>
{
    public PatchAnnouncementModelValidator(CategoryFieldsValidationService categoryFieldsValidationService)
    {
        When(model => model.Fields != null && !string.IsNullOrWhiteSpace(model.CategoryId), () =>
        {
            RuleFor(p => new { p.Fields, p.CategoryId} )
                .Must(x => categoryFieldsValidationService.ValidateByCategory(x.Fields.Names.ToArray(), x.CategoryId))
                .WithMessage("Some incorrect fields or category");
        });
    }
}