using FluentValidation;
using JetBrains.Annotations;
using UAE.Application.Models.Announcement;
using UAE.Application.Services.Validation.Implementation;

namespace UAE.Application.Validations.ValidationRules;

[UsedImplicitly]
public class UpdateAnnouncementModelValidator : AbstractValidator<UpdateAnnouncementModel>
{
    public UpdateAnnouncementModelValidator(CategoryFieldsValidationService categoryFieldsValidationService)
    {
        RuleFor(p => new { p.Fields, p.CategoryId} )
            .Must(x => categoryFieldsValidationService.DoesFieldExistInAllCategories(x.Fields.Names.ToArray(), new []{ x.CategoryId } ));
    }
}