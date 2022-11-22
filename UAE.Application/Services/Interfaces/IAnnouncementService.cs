using UAE.Application.Models.Order;
using UAE.Shared;

namespace UAE.Application.Services.Interfaces;

public interface IAnnouncementService
{
    Task<AnnouncementModel> CreateAnnouncement(AnnouncementModel announcementModel);
    
    Task UpdateAnnouncementAsync(AnnouncementModel announcement);
    
    Task DeleteAnnouncementAsync(int id);
    
    Task<AnnouncementModel> GetAnnouncementByIdAsync(int id);

    Task<PagedResponse<AnnouncementModel>> SearchAnnouncement(SearchAnnouncementModel searchAnnouncementModel);
}