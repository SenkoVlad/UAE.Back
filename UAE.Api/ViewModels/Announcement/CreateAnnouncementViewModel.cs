namespace UAE.Api.ViewModels.Announcement;

public record CreateAnnouncementViewModel(
    string Description,
    string Title,
    string CategoryId,
    string CurrencyCode,
    decimal Price,
    string Fields,
    string AddressToTake,
    string Address,
    List<IFormFile> Pictures
);