using FluentValidation;
using UAE.Application.Models.User;

namespace UAE.Api.Validations.ValidationRules;

public class CreateUserModelValidator : AbstractValidator<CreateUserModel>
{
    public CreateUserModelValidator()
    {   
        RuleFor(c => c.Email)
            .EmailAddress();

        RuleFor(c => c.Password)
            .MinimumLength(8);
    }
}