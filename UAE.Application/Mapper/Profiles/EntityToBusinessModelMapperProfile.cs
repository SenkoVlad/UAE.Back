using AutoMapper;
using UAE.Application.Models.Announcement;
using UAE.Application.Models.Category;
using UAE.Application.Models.User;
using UAE.Core.Entities;

namespace UAE.Application.Mapper.Profiles;

public sealed class EntityToBusinessModelMapperProfile : Profile
{
    public EntityToBusinessModelMapperProfile()
    {
        CreateMap<Announcement, AnnouncementModel>()
            .ReverseMap();
        
        CreateMap<CreateUserModel, User>()
            .ReverseMap();

        CreateMap<CategoryModel, Category>()
            .ReverseMap();

        CreateMap<CreateAnnouncementModel, Announcement>()
            .ForMember(c => c.Category, o => o.MapFrom(c => c.CategoryId))
            .ReverseMap();
    }
}