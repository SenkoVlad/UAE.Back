namespace UAE.Application.Models.Announcement;

public sealed record UpdateAnnouncementModel(
    string EntityId,
    Dictionary<string, object> FieldsValuesToUpdate);
