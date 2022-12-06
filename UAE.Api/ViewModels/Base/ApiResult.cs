using FluentValidation.Results;

namespace UAE.Api.ViewModels.Base;

public sealed class ApiResult<T>
{
    private ApiResult(bool succeeded, T result, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Result = result;
        Errors = errors;
    }

    public bool Succeeded { get; }

    public T Result { get; }

    public IEnumerable<string> Errors { get; }

    public static ApiResult<T> Success(T result) => 
        new(true, result, new List<string>());

    public static ApiResult<T> Failure(IEnumerable<string> errors) =>
        new(false, default, errors);

    public static ApiResult<T> ValidationFailure(List<ValidationFailure> errors)
    {
        var errorMessages = errors.Select(v => string.Concat(v.PropertyName, ":", v.ErrorMessage));
        
        return new(false, default, errorMessages);
    }
}