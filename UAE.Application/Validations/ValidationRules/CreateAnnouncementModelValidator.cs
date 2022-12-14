using FluentValidation;
using JetBrains.Annotations;
using UAE.Application.Models.Announcement;
using UAE.Application.Services.Validation.Interfaces;

namespace UAE.Application.Validations.ValidationRules;

[UsedImplicitly]
public class CreateAnnouncementModelValidator : AbstractValidator<CreateAnnouncementModel>
{
    public CreateAnnouncementModelValidator(ICategoryFieldsValidationService categoryFieldsValidationService)
    {
        RuleFor(p => new { p.Fields, p.CategoryId} )
            .Must(x => categoryFieldsValidationService.DoesFieldExistInAllCategories(x.Fields.Names.ToArray(), new []{ x.CategoryId }))
            .WithMessage("Fields have incorrect format");
    }
}