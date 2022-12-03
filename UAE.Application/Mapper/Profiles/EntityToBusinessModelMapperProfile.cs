using System.Text.Json;
using AutoMapper;
using DnsClient.Protocol;
using UAE.Application.Models.Announcement;
using UAE.Application.Models.Category;
using UAE.Application.Models.User;
using UAE.Core.Entities;

namespace UAE.Application.Mapper.Profiles;

public sealed class EntityToBusinessModelMapperProfile : Profile
{
    public EntityToBusinessModelMapperProfile()
    {
        CreateMap<CreateAnnouncementModel, Announcement>()
            .ForMember(dest => dest.Fields, 
                o => o.MapFrom(src => CovertAnnouncementField(src.Fields)))
            .ForMember(dest => dest.Category,
                o => o.MapFrom(src => src.CategoryId))
            .ReverseMap();
        
        CreateMap<AnnouncementModel, Announcement>()
            .ForMember(dest => dest.Fields, 
                o => o.MapFrom(src => CovertAnnouncementField(src.Fields)))
            .ForMember(dest => dest.Category,
                o => o.MapFrom(src => src.CategoryId))
            .ReverseMap();

        CreateMap<CreateUserModel, User>()
            .ReverseMap();

        CreateMap<CategoryModel, Category>()
            .ReverseMap();
    }

    private Dictionary<string,object> CovertAnnouncementField(Dictionary<string,object> srcFields)
    {
        var destFields = new Dictionary<string, object>();
        
        foreach (var fieldsKey in srcFields.Keys)
        {
            var value = srcFields[fieldsKey];
            var jsonValueType = value is JsonElement element ? element : default;
            
            switch (jsonValueType.ValueKind.ToString())
            {
                case "Number": 
                    destFields.Add(fieldsKey, jsonValueType.GetInt32());
                    break;
                case "String":
                    destFields.Add(fieldsKey, jsonValueType.GetString()!);
                    break;
                default:
                    destFields.Add(fieldsKey, value);
                    break;
            }
        }

        return destFields;
    }
}
