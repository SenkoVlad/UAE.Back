namespace UAE.Api.ViewModels.Announcement;

public record UpdateAnnouncementViewModel(
    string Id,
    string Title,
    string Description,
    string CategoryId,
    string Fields, 
    string AddressToTake, 
    string Address, 
    long CreatedDateTime);