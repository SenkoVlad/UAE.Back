namespace UAE.Api.ViewModels.Announcement;

public record PatchAnnouncementViewModel(
    string Id,
    string? Title,
    string? Description,
    string CategoryId,
    string? Fields, 
    string? AddressToTake, 
    string? Address);