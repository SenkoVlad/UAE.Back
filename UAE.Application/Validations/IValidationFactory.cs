using FluentValidation;

namespace UAE.Application.Validations;

public interface IValidationFactory
{
    IValidator<T> GetValidator<T>();
}