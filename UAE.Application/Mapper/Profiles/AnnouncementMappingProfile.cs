using UAE.Application.Extensions;
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
            Fields = model.Fields.ToDictionaryWithCheckingForValueKind(),
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
            Fields = model.Fields.ToDictionaryWithCheckingForValueKind(),
            Title = model.Title,
            CreatedDateTime = model.CreatedDateTime,
            LastUpdateDateTime = model.LastUpdateDateTime
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

}