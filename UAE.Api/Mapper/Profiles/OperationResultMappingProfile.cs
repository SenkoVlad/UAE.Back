using UAE.Api.ViewModels.Base;
using UAE.Application.Models;

namespace UAE.Api.Mapper.Profiles;

public static class OperationResultMappingProfile
{
    public static ApiResult<IEnumerable<string>> ToApiResult(this OperationResult operationResult)
    {
        return operationResult.IsSucceed
            ? ApiResult<IEnumerable<string>>.Success(operationResult.ResultMessages)
            : ApiResult<IEnumerable<string>>.Failure(errors: operationResult.ResultMessages);
    }
}