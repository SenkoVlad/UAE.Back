using UAE.Application.Models;
using UAE.Application.Models.Announcement;
using UAE.Core.Entities;
using UAE.Shared;

namespace UAE.Application.Services.Interfaces;

public interface IAnnouncementService
{
    Task<OperationResult<Announcement>> CreateAnnouncement(CreateAnnouncementModel createAnnouncementModel);

    Task<OperationResult<Announcement>> UpdateAnnouncementAsync(UpdateAnnouncementModel updateAnnouncementModel);    
    
    Task<OperationResult<Announcement>> PatchAnnouncementAsync(PatchAnnouncementModel patchAnnouncementModel);    
    
    Task<OperationResult<string>> DeleteAnnouncementAsync(string id);
    
    Task<PagedResponse<Announcement>> SearchAnnouncement(SearchAnnouncementModel searchAnnouncementModel);
    
    Task<OperationResult<Announcement>> GetByIdAsync(string announcementId);
}