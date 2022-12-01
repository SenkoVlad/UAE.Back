namespace UAE.Application.Models;

public record OperationResult(
    IEnumerable<string> ResultMessages,
    bool IsSucceed);
