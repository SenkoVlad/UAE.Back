using FluentValidation;

namespace UAE.Api.Validations;

public class ValidationFactory : IValidationFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IValidator<T> GetValidator<T>()
    {
        return _serviceProvider.GetRequiredService<IValidator<T>>();
    }
}