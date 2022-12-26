namespace UAE.Api.ViewModels.Announcement;

public record UpdateAnnouncementViewModel(
    string Id,
    string Title,
    string Description,
    string CategoryId,
    string CurrencyCode,
    decimal Price,
    string Fields, 
    string AddressToTake, 
    string Address, 
    long CreatedDateTime,
    List<IFormFile>? Pictures
);