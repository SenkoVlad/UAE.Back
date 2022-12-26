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
    public SearchAnnouncementModelValidator(ICategoryFieldsValidationService categoryFieldsValidationService,
        IFilterFieldsValidationService filterFieldsValidationService)
    {
        When(model => model.Description?.FieldValue != null, () =>
        {
            RuleFor(p => p.Description!.FieldValue)
                .MaximumLength(200);
        });

        SortedByValidator(categoryFieldsValidationService);
        ExtraFieldsValidator(categoryFieldsValidationService, filterFieldsValidationService);
    }

    private void ExtraFieldsValidator(ICategoryFieldsValidationService categoryFieldsValidationService,
        IFilterFieldsValidationService filterFieldsValidationService)
    {
        When(model => model.Filters.Elements.Any(), () =>
        {
            RuleFor(p => new {FieldNames = p.Filters.Names.ToArray(), p.CategoryId})
                .Must(x => categoryFieldsValidationService.ValidateByCategory(x.FieldNames, x.CategoryId))
                .WithMessage("Name of filter field is not valid");

            RuleFor(p => p.Filters.Values)
                .Must(filterFieldsValidationService.ValidateFilterField)
                .WithMessage("Filters are not valid");
        });
    }

    private void SortedByValidator(ICategoryFieldsValidationService categoryFieldsValidationService)
    {
        When(model => !string.IsNullOrWhiteSpace(model.CategoryId) 
                          && !string.IsNullOrWhiteSpace(model.SortedBy), () =>
        {
            RuleFor(p => new AnnouncementFieldParameter
                {
                    CategoryId = p.CategoryId!,
                    FieldName = p.SortedBy!
                })
                .SetValidator(
                    new AnnouncementFieldsValidator<SearchAnnouncementModel, AnnouncementFieldParameter>(
                        categoryFieldsValidationService))
                .WithMessage("Sorted by field is not valid");
        });
    }
}