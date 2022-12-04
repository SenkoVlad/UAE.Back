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
            .NotNull()
            .NotEmpty();
        
        RuleFor(c => c.Description)
            .NotNull()
            .NotEmpty();
        
        RuleFor(c => c.Address)
            .NotNull()
            .NotEmpty();
        
        RuleFor(c => c.CategoryId)
            .NotNull()
            .NotEmpty();
        
        RuleFor(c => c.Fields)
            .NotNull()
            .NotEmpty();
    }
}