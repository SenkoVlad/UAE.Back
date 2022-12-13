using UAE.Application.Models.Announcement;
using UAE.Core.Entities;

namespace UAE.Application.Mapper.Profiles;

public static class AnnouncementMappingProfile
{
    public static Announcement ToEntity(this CreateAnnouncementModel model)
    {
        return new Announcement
        {
            Address = model.Address,
            Category = model.CategoryId,
            Description = model.Description,
            Fields = model.Fields,
            Title = model.Title
        };
    }
    
    public static Announcement ToEntity(this AnnouncementModel model)
    {
        return new Announcement
        {
            ID = model.Id,
            Address = model.Address,
            Category = model.CategoryId,
            Description = model.Description,
            Fields = model.Fields,
            Title = model.Title,
            CreatedDateTime = model.CreatedDateTime,
            LastUpdateDateTime = model.LastUpdateDateTime
        };
    }
    
    public static Announcement ToEntity(this PatchAnnouncementModel model)
    {
        return new Announcement
        {
            ID = model.Id,
            Address = string.IsNullOrWhiteSpace(model.Address)
                ? null! 
                : model.Address,
            AddressToTake = string.IsNullOrWhiteSpace(model.AddressToTake)
                ? null!
                : model.AddressToTake,
            Category = model.CategoryId,
            Description = string.IsNullOrWhiteSpace(model.Description)
                ? null! 
                : model.Description,
            Fields = model.Fields,
            Title = string.IsNullOrWhiteSpace(model.Title)
                ? null! 
                : model.Title,
        };
    }

    public static AnnouncementModel ToBusinessModel(this Announcement model)
    {
        return new AnnouncementModel(
            Id : model.ID,
            Address : model.Address,
            CategoryId : model.Category.ID,
            Description : model.Description,
            Fields : model.Fields,
            Title : model.Title,
            CreatedDateTime : model.CreatedDateTime,
            LastUpdateDateTime : model.LastUpdateDateTime,
            AddressToTake: model.AddressToTake
        );
    }
    
    public static Announcement ToEntity(this UpdateAnnouncementModel model)
    {
        return new Announcement
        {
            ID = model.Id,
            Address = model.Address,
            Category = model.CategoryId,
            Description = model.Description,
            Fields = model.Fields,
            Title = model.Title,
            CreatedDateTime = model.CreatedDateTime,
            AddressToTake = model.AddressToTake
        };
    }
}