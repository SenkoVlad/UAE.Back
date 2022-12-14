using MongoDB.Bson;
using Newtonsoft.Json;
using UAE.Api.ViewModels.Announcement;
using UAE.Application.Mapper.Profiles;
using UAE.Application.Models.Announcement;
using UAE.Core.Entities;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace UAE.Api.Mapper.Profiles;

public static class AnnouncementMappingProfile
{
    public static SearchAnnouncementModel ToBusinessModel(this SearchAnnouncementViewModel model)
    {
        return new SearchAnnouncementModel
        (
            Description: model.Description,
            CategoryIds: model.CategoryIds != null && model.CategoryIds.Any()
                ? model.CategoryIds.ToList()
                : new List<string>(),
            PageNumber: model.PageNumber,
            PageSize: model.PageSize,
            Filters: model.Filters,
            SortedBy: model.SortedBy ?? nameof(AnnouncementModel.CreatedDateTime),
            Price: model.Price
        );
    }
    
    public static CreateAnnouncementModel ToBusinessModel(this CreateAnnouncementViewModel model)
    {
        return new CreateAnnouncementModel
        (
            Description: model.Description,
            CategoryId: model.CategoryId,
            Title: model.Title,
            Fields: string.IsNullOrWhiteSpace(model.Fields) 
                ? new BsonDocument()
                : BsonDocument.Parse(model.Fields),
            AddressToTake: model.AddressToTake,
            Address: model.Address,
            Pictures: model.Pictures,
            CurrencyCode: model.CurrencyCode,
            Price: model.Price
        );
    }
    
    public static AnnouncementViewModel ToViewModel(this Announcement model)
    {
        return new AnnouncementViewModel
        (
            Description: model.Description,
            CategoryId: model.Category.ID,
            Fields:model.Fields.ToJson(),
            Id: model.ID,
            Title: model.Title,
            Address: model.Address,
            AddressToTake: model.AddressToTake,
            CreatedDateTime: model.CreatedDateTime,
            LastUpdateDateTime: model.LastUpdateDateTime,
            CategoryPath: model.CategoryPath
                .Select(c => c.ToBusinessModel())
                .ToArray(),
            Pictures: model.Pictures
                .Select(p => p.ToBusinessModel())
                .ToArray(),
            Price: model.Price,
            CurrencyCode: model.Currency.Code
        );
    }
    
    public static PatchAnnouncementModel ToBusinessModel(this PatchAnnouncementViewModel model)
    {
        return new PatchAnnouncementModel
        (
            Description: model.Description,
            CategoryId: model.CategoryId,
            Fields: string.IsNullOrWhiteSpace(model.Fields) 
                ? new BsonDocument()
                : BsonDocument.Parse(model.Fields),
            Id: model.Id,
            Title: model.Title,
            Address: model.Address,
            AddressToTake: model.AddressToTake,
            Pictures: model.Pictures,
            CurrencyCode: model.CurrencyCode,
            Price: model.Price
        );
    }

    public static AnnouncementViewModel ToViewModel(this AnnouncementModel model)
    {
        return new AnnouncementViewModel
        (
            Description: model.Description,
            CategoryId: model.CategoryId,
            Fields: model.Fields.ToJson(),
            Id: model.Id,
            Title: model.Title,
            Address: model.Address,
            AddressToTake: model.AddressToTake,
            CreatedDateTime: model.CreatedDateTime,
            LastUpdateDateTime: model.LastUpdateDateTime,
            CategoryPath: model.CategoryPath,
            Pictures: model.Pictures,
            CurrencyCode: model.CurrencyCode,
            Price: model.Price
        );
    }
    
    public static UpdateAnnouncementModel ToBusinessModel(this UpdateAnnouncementViewModel model)
    {
        return new UpdateAnnouncementModel
        (
            Description: model.Description,
            CategoryId: model.CategoryId,
            Fields: string.IsNullOrWhiteSpace(model.Fields) 
                ? new BsonDocument()
                : BsonDocument.Parse(model.Fields),
            Id: model.Id,
            Title: model.Title,
            Address: model.Address,
            AddressToTake: model.AddressToTake,
            CreatedDateTime: model.CreatedDateTime,
            CurrencyCode: model.CurrencyCode,
            Price: model.Price,
            Pictures: model.Pictures ?? new List<IFormFile>()
        );
    }

    public static AnnouncementBrowsingHistoryViewModel ToViewModel(this AnnouncementBrowsingHistoryModel model)
    {
        return new AnnouncementBrowsingHistoryViewModel(
            Announcement: model.Announcement.ToViewModel(),
            ViewDateTimeUtc: model.ViewDateTimeUtc
        );
    }
}