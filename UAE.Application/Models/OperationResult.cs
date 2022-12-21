namespace UAE.Application.Models;

public record OperationResult<T>(
    IEnumerable<string> ResultMessages,
    bool IsSucceed,
    T Result = default!);
