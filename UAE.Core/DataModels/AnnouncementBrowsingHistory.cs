namespace UAE.Core.DataModels;

public sealed record AnnouncementBrowsingHistory(
    string AnnouncementId,
    long ViewDateTimeUtc);