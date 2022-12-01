using AutoMapper;
using UAE.Api.ViewModels.AnnouncementViewModels;
using UAE.Application.Models;
using UAE.Application.Models.Announcement;
using UAE.Application.Models.Base;

namespace UAE.Api.Mapper.Profiles;

public class ApiToBusinessModelMapperProfile : Profile
{
    public ApiToBusinessModelMapperProfile()
    {
        CreateMap<AnnouncementViewModel, AnnouncementModel>()
            .ReverseMap();

        CreateMap<ApiResult<IEnumerable<string>>, OperationResult>()
            .ForMember(dest => dest.IsSucceed,
                opt => opt.MapFrom(o => o.Succeeded))
            .ForMember(dest => dest.ResultMessages,
                opt => opt.MapFrom(src => src.Succeeded
                    ? src.Result
                    : src.Errors));
    }
}