using AutoMapper;
using UAE.Api.ViewModels.AnnouncementViewModels;
using UAE.Application.Models.Announcement;

namespace UAE.Api.Mapper.Profiles;

public class ApiToBusinessModelMapperProfile : Profile
{
    public ApiToBusinessModelMapperProfile()
    {
        CreateMap<AnnouncementViewModel, AnnouncementModel>()
            .ReverseMap();            
    }
}