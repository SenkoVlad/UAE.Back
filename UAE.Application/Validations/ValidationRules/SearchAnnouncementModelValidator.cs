using FluentValidation;
using JetBrains.Annotations;
using UAE.Application.Models.Announcement;
using UAE.Application.Services.Validation.Interfaces;
using UAE.Application.Validations.CustomValidators;
using UAE.Application.Validations.ValidationParameters;

namespace UAE.Application.Validations.ValidationRules;

[UsedImplicitly]
public class SearchAnnouncementModelValidator : AbstractValidator<SearchAnnouncementModel>
{
    public SearchAnnouncementModelValidator(ICategoryFieldsValidationService categoryFieldsValidationService)
    {
        When(model => model.Description != null, () =>
        {
            RuleFor(p => p.Description)
                .MaximumLength(20);
        });

        SortedByValidator(categoryFieldsValidationService);
        ExtraFieldsValidator(categoryFieldsValidationService);
    }

    private void ExtraFieldsValidator(ICategoryFieldsValidationService categoryFieldsValidationService)
    {
        When(model => model.Filters != null && model.Filters.Keys.Any(), () =>
        {
            RuleFor(p => new {FieldNames = p.Filters!.Keys.ToArray(), CategoryId = p.CategoryIds})
                .Must(x => categoryFieldsValidationService.DoesFieldExistInAllCategories(x.FieldNames, x.CategoryId.ToArray()))
                .WithMessage("Name of filter field is not valid");
        });
    }

    private void SortedByValidator(ICategoryFieldsValidationService categoryFieldsValidationService)
    {
        When(model => model.CategoryIds != null && model.CategoryIds.Any() 
                          && !string.IsNullOrWhiteSpace(model.SortedBy), () =>
        {
            RuleFor(p => new AnnouncementFieldParameter
                {
                    CategoryIds = p.CategoryIds!.ToArray(),
                    FieldName = p.SortedBy!
                })
                .SetValidator(
                    new AnnouncementFieldsValidator<SearchAnnouncementModel, AnnouncementFieldParameter>(
                        categoryFieldsValidationService))
                .WithMessage("Sorted by field is not valid");
        });
    }
}