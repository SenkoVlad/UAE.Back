using FluentValidation;
using UAE.Application.Models.Announcement;
using UAE.Application.Validation;

namespace UAE.Application.Validations.ValidationRules;

public class CreateAnnouncementModelValidator : AbstractValidator<CreateAnnouncementModel>
{
    public CreateAnnouncementModelValidator(CategoryFieldsValidationService categoryFieldsValidationService)
    {
        RuleFor(c => c.Title)
            .NotEmpty();
        
        RuleFor(c => c.Description)
            .NotEmpty();
        
        RuleFor(c => c.Address)
            .NotEmpty();
        
        RuleFor(c => c.CategoryId)
            .NotEmpty();
        
        RuleFor(c => c.Fields)
            .NotEmpty();
        
        RuleFor(p => new { p.Fields, p.CategoryId} )
            .Must(x => categoryFieldsValidationService.ValidateByCategory(x.Fields.Names.ToArray(), x.CategoryId));
    }
}