using FluentValidation;
using UAE.Api.Validations.CustomValidators;
using UAE.Application.Models.Order;

namespace UAE.Api.Validations.ValidationRules;

public class SearchAnnouncementModelValidator : AbstractValidator<SearchAnnouncementModel>
{
    public SearchAnnouncementModelValidator()
    {
        RuleFor(p => p.Description)
            .NotNull()
            .MaximumLength(200);

        RuleFor(p => p.SortedBy)
            .SetValidator(new SearchAnnouncementsSortedFieldValidator<SearchAnnouncementModel, string>());
    }
}