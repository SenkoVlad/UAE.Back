using AutoMapper;
using UAE.Api.ViewModels.AnnouncementViewModels;
using UAE.Application.Models.Order;

namespace UAE.Api.Mapper.Profiles
{
    public class ApiToBusinessModelMapperProfile : Profile
    {
        public ApiToBusinessModelMapperProfile()
        {
            CreateMap<AnnouncementViewModel, AnnouncementModel>()
                .ReverseMap();            
        }
    }
}