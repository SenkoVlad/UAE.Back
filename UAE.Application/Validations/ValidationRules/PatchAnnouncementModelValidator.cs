using FluentValidation;
using JetBrains.Annotations;
using UAE.Application.Models.Announcement;
using UAE.Application.Services.Validation.Implementation;

namespace UAE.Application.Validations.ValidationRules;

[UsedImplicitly]
public class PatchAnnouncementModelValidator : AbstractValidator<PatchAnnouncementModel>
{
    public PatchAnnouncementModelValidator(CategoryFieldsValidationService categoryFieldsValidationService)
    {
        When(model => model.Fields != null 
                      && model.Fields.Elements.Any()
                      && !string.IsNullOrWhiteSpace(model.CategoryId), () =>
        {
            RuleFor(p => new { p.Fields, p.CategoryId} )
                .Must(x => categoryFieldsValidationService.ValidateByCategory(x.Fields.Names.ToArray(), x.CategoryId))
                .WithMessage("Some incorrect fields or category");
        });
    }
}