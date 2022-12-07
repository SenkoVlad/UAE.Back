using FluentValidation;
using UAE.Application.Models.Announcement;
using UAE.Application.Validation;
using UAE.Application.Validations.CustomValidators;

namespace UAE.Application.Validations.ValidationRules;

public class SearchAnnouncementModelValidator : AbstractValidator<SearchAnnouncementModel>
{
    public SearchAnnouncementModelValidator(CategoryFieldsValidationService categoryFieldsValidationService)
    {
        RuleFor(p => p.Description)
            .MaximumLength(200);

        RuleFor(p => p.SortedBy)
            .SetValidator(new AnnouncementFieldsValidator<SearchAnnouncementModel, string>());

        When(model => model.Fields != null, () =>
        {
            RuleFor(p => new { p.Fields, p.CategoryId} )
                .Must(x => categoryFieldsValidationService.ValidateByCategory(x.Fields.Keys.ToArray(), x.CategoryId));
        });
    }
}