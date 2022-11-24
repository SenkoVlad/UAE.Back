using AutoMapper;
using UAE.Application.Models.Order;
using UAE.Core.Entities;

namespace UAE.Application.Mapper.Profiles;

public sealed class EntityToBusinessModelMapperProfile : Profile
{
    public EntityToBusinessModelMapperProfile()
    {
        CreateMap<Announcement, AnnouncementModel>()
            .ReverseMap();
    }
}