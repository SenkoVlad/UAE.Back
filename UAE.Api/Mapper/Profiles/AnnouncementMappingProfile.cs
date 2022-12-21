using MongoDB.Bson;
using UAE.Api.ViewModels.Announcement;
using UAE.Application.Models.Announcement;
using UAE.Core.Entities;

namespace UAE.Api.Mapper.Profiles;

public static class AnnouncementMappingProfile
{
    public static SearchAnnouncementModel ToBusinessModel(this SearchAnnouncementViewModel model)
    {
        return new SearchAnnouncementModel
        (
            Description: model.Description,
            CategoryId: model.CategoryId,
            PageNumber: model.PageNumber,
            PageSize: model.PageSize,
            Filters: string.IsNullOrWhiteSpace(model.Filters) 
                ? new BsonDocument()
                : BsonDocument.Parse(model.Filters),
            SortedBy: model.SortedBy
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
            Pictures: model.Pictures
        );
    }
    
    public static AnnouncementModel ToBusinessModel(this AnnouncementViewModel model)
    {
        return new AnnouncementModel
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
            LastUpdateDateTime: model.LastUpdateDateTime
        );
    }
    
    public static AnnouncementViewModel ToViewModel(this Announcement model)
    {
        return new AnnouncementViewModel
        (
            Description: model.Description,
            CategoryId: model.Category.ID,
            Fields: model.Fields != null 
                ? model.Fields.ToJson()
                : string.Empty,
            Id: model.ID,
            Title: model.Title,
            Address: model.Address,
            AddressToTake: model.AddressToTake,
            CreatedDateTime: model.CreatedDateTime,
            LastUpdateDateTime: model.LastUpdateDateTime
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
            Pictures: model.Pictures
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
            LastUpdateDateTime: model.LastUpdateDateTime
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
            CreatedDateTime: model.CreatedDateTime
        );
    }
}