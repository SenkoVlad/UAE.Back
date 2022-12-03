namespace UAE.Application.Models;

public record OperationResult(
    IEnumerable<string> ResultMessages,
    IEnumerable<string> Errors,
    bool IsSucceed);
