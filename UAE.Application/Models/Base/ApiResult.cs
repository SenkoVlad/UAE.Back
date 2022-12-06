using System.Collections.Generic;

namespace UAE.Application.Models.Base;

public sealed class ApiResult<T>
{
    private ApiResult(bool succeeded, T result, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Result = result;
        Errors = errors;
    }

    public ApiResult()
    {
    }

    public bool Succeeded { get; set; }

    public T Result { get; set; }

    public IEnumerable<string> Errors { get; set; }

    public static ApiResult<T> Success(T result) => 
        new(true, result, new List<string>());

    public static ApiResult<T> Failure(IEnumerable<string> errors) =>
        new(false, default, errors);
}