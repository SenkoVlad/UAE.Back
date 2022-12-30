using FluentValidation.Results;

namespace UAE.Api.ViewModels.Base;

public sealed class ApiResult<T>
{
    private ApiResult(bool succeeded, T result, IEnumerable<string> errors, IEnumerable<string>? resultMessage)
    {
        Succeeded = succeeded;
        Result = result;
        Errors = errors;
        ResultMessage = resultMessage;
    }

    public bool Succeeded { get; }

    public T Result { get; }
    
    public IEnumerable<string>? ResultMessage { get; }

    public IEnumerable<string> Errors { get; }

    public static ApiResult<T> Success(IEnumerable<string>? resultMessage, T result) => 
        new(true, result, new List<string>(), resultMessage);

    public static ApiResult<T> Failure(IEnumerable<string> errors) =>
        new(false, default, errors, default);

    public static ApiResult<T> ValidationFailure(List<ValidationFailure> errors)
    {
        var errorMessages = errors.Select(v => string.Concat(v.PropertyName, ":", v.ErrorMessage));
        
        return new(false, default, errorMessages, resultMessage: default);
    }

    public override string ToString()
    {
        if (Errors.Any())
        {
            return string.Join(", ", Errors);
        }

        if (ResultMessage != null && ResultMessage.Any())
        {
            return string.Join(", ", ResultMessage);
        }

        return string.Empty;
    }
}