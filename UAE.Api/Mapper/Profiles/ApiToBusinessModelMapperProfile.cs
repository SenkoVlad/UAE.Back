using AutoMapper;
using UAE.Application.Models;
using UAE.Application.Models.Base;

namespace UAE.Api.Mapper.Profiles;

public class ApiToBusinessModelMapperProfile : Profile
{
    public ApiToBusinessModelMapperProfile()
    {
        CreateMap<OperationResult, ApiResult<IEnumerable<string>>>()
            .ForMember(dest => dest.Succeeded,
                opt => opt.MapFrom(o => o.IsSucceed))
            .ForMember(dest => dest.Succeeded 
                ? dest.Result 
                : dest.Errors,
                opt => opt.MapFrom(src => src.ResultMessages))
            .ReverseMap();
    }
}