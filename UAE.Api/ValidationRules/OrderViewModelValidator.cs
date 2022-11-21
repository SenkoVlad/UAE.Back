using FluentValidation;
using UAE.Api.ViewModels.OrderViewModels;

namespace UAE.Api.ValidationRules;

public class OrderViewModelValidator : AbstractValidator<OrderViewModel>
{
    public OrderViewModelValidator()
    {
        RuleFor(p => p.Description)
            .NotNull()
            .MaximumLength(200);

        RuleFor(p => p.Title)
            .NotNull()
            .MaximumLength(20);
    }
}