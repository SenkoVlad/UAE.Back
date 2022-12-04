using FluentValidation;
using UAE.Api.Validations.CustomValidators;
using UAE.Application.Models.Announcement;
using UAE.Core.Entities;

namespace UAE.Api.Validations.ValidationRules;

public class CreateAnnouncementModelValidator : AbstractValidator<CreateAnnouncementModel>
{
    public CreateAnnouncementModelValidator()
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
    }
}