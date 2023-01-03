using UAE.Api.ViewModels.Base;
using UAE.Application.Models;

namespace UAE.Api.Mapper.Profiles;

public static class OperationResultMappingProfile
{
    public static ApiResult<TDest> ToApiResult<TSource, TDest>(this OperationResult<TSource> operationResult,
        Func<TDest> mapperResultToApiResultFunc)
    {
        return operationResult.IsSucceed
            ? ApiResult<TDest>.Success(operationResult.ResultMessages, mapperResultToApiResultFunc.Invoke())
            : ApiResult<TDest>.Failure(errors: operationResult.ResultMessages ?? new []{ string.Empty });
    }
}