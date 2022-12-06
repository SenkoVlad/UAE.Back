using FluentValidation;

namespace UAE.Api.Validations;

public interface IValidationFactory
{
    IValidator<T> GetValidator<T>();
}