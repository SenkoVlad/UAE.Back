using FluentValidation;
using UAE.Application.Models.Announcement;
using UAE.Application.Validation;
using UAE.Application.Validations.CustomValidators;
using UAE.Application.Validations.ValidationParameters;

namespace UAE.Application.Validations.ValidationRules;

public class SearchAnnouncementModelValidator : AbstractValidator<SearchAnnouncementModel>
{
    public SearchAnnouncementModelValidator(CategoryFieldsValidationService categoryFieldsValidationService)
    {
        RuleFor(p => p.Description)
            .MaximumLength(200);

        RuleFor(p => new AnnouncementSortByParameter
            {
                CategoryId = p.CategoryId,
                SortedBy = p.SortedBy
            })
            .SetValidator(new AnnouncementFieldsValidator<SearchAnnouncementModel, AnnouncementSortByParameter>(categoryFieldsValidationService));
        
        When(model => model.Fields != null, () =>
        {
            RuleFor(p => new { p.Fields, p.CategoryId} )
                .Must(x => categoryFieldsValidationService.ValidateByCategory(x.Fields.Keys.ToArray(), x.CategoryId));
        });
    }
}