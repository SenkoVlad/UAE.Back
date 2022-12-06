namespace UAE.Application.Models.Announcement;

public sealed record PatchAnnouncementModel(
    string Id,
    string? Title,
    string? Description,
    string? CategoryId,
    Dictionary<string, object>? Fields, 
    string? AddressToTake, 
    string? Address);