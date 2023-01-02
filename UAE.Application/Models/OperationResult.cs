namespace UAE.Application.Models;

public record OperationResult<T>(bool IsSucceed,
    T Result = default!, IEnumerable<string>? ResultMessages = null);
