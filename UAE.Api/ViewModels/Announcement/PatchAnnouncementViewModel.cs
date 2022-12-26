namespace UAE.Api.ViewModels.Announcement;

public record PatchAnnouncementViewModel(
    string Id,
    string? Title,
    string? Description,
    string? CurrencyCode,
    decimal? Price,
    string? CategoryId,
    string? Fields, 
    string? AddressToTake, 
    string? Address,
    List<IFormFile>? Pictures);