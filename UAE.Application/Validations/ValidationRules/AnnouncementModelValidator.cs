using FluentValidation;
using JetBrains.Annotations;
using UAE.Application.Models.Announcement;
using UAE.Application.Services.Validation.Implementation;

namespace UAE.Application.Validations.ValidationRules;

[UsedImplicitly]
public class AnnouncementModelValidator : AbstractValidator<AnnouncementModel>
{
    public AnnouncementModelValidator(CategoryFieldsValidationService categoryFieldsValidationService)
    {
        RuleFor(p => new { p.Fields, p.CategoryId} )
            .Must(x => categoryFieldsValidationService.ValidateByCategory(x.Fields.Names.ToArray(), x.CategoryId));
    }
}