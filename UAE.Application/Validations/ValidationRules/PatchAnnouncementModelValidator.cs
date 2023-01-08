using FluentValidation;
using JetBrains.Annotations;
using UAE.Application.Models.Announcement;
using UAE.Application.Services.Validation.Interfaces;

namespace UAE.Application.Validations.ValidationRules;

[UsedImplicitly]
public class PatchAnnouncementModelValidator : AbstractValidator<PatchAnnouncementModel>
{
    public PatchAnnouncementModelValidator(ICategoryFieldsValidationService categoryFieldsValidationService)
    {
        When(model => model.Fields != null 
                      && model.Fields.Elements.Any()
                      && !string.IsNullOrWhiteSpace(model.CategoryId), () =>
        {
            RuleFor(p => new { p.Fields, p.CategoryId} )
                .Must(x => categoryFieldsValidationService.DoesFieldExistInAllCategories(x.Fields.Names.ToArray(), new []{ x.CategoryId }))
                .WithMessage("Some incorrect fields or category");
        });
    }
}