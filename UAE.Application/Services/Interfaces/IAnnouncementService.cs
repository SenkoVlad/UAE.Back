using UAE.Application.Models;
using UAE.Application.Models.Announcement;
using UAE.Shared;

namespace UAE.Application.Services.Interfaces;

public interface IAnnouncementService
{
    Task<OperationResult> CreateAnnouncement(CreateAnnouncementModel createAnnouncementModel);

    Task<OperationResult> UpdateAnnouncementAsync(AnnouncementModel announcementModel);    
    Task DeleteAnnouncementAsync(int id);
    
    Task<AnnouncementModel> GetAnnouncementByIdAsync(int id);

    Task<PagedResponse<AnnouncementModel>> SearchAnnouncement(SearchAnnouncementModel searchAnnouncementModel);
}