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
            AddressToTake = model.AddressToTake,
            Category = model.CategoryId,
            Description = model.Description,
            Fields = model.Fields,
            Title = model.Title,
            Price = model.Price,
            Currency = model.CurrencyId
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
            Fields = model.Fields != null 
                ? model.Fields 
                : null!,
            Title = string.IsNullOrWhiteSpace(model.Title)
                ? null! 
                : model.Title,
            Price = model.Price ?? default,
            Currency = model.CurrencyId
        };
    }

    public static AnnouncementModel ToBusinessModel(this Announcement model)
    {
        return new AnnouncementModel(
            Id: model.ID,
            Address: model.Address,
            CategoryId: model.Category.ID,
            Description: model.Description,
            Fields: model.Fields,
            Title: model.Title,
            CreatedDateTime: model.CreatedDateTime,
            LastUpdateDateTime: model.LastUpdateDateTime,
            AddressToTake: model.AddressToTake,
            CategoryPath: model.CategoryPath
                .Select(c => c.ToBusinessModel())
                .ToArray(),
            Pictures: model.Pictures
                .Select(p => p.ToBusinessModel())
                .ToArray(),
            CurrencyId: model.Currency.ID,
            Price: model.Price);
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
            AddressToTake = model.AddressToTake,
            Price = model.Price,
            Currency = model.CurrencyId
        };
    }
}