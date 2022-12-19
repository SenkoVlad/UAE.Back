using MongoDB.Entities;
using UAE.Application.Mapper.Profiles;
using UAE.Application.Models;
using UAE.Application.Models.Announcement;
using UAE.Application.Services.Interfaces;
using UAE.Core.Entities;
using UAE.Core.Repositories;
using UAE.Shared;
using UAE.Shared.Enum;

namespace UAE.Application.Services.Implementations;

internal sealed class AnnouncementService : IAnnouncementService
{
    private readonly IAnnouncementRepository _announcementRepository;
    private readonly IUserService _userService;
    private readonly IPagedQueryBuilderService<Announcement> _searchPagedQueryBuilder;
    
    public AnnouncementService(IAnnouncementRepository announcementRepository, 
        IUserService userService, 
        IPagedQueryBuilderService<Announcement> searchPagedQueryBuilder)
    {
        _announcementRepository = announcementRepository;
        _userService = userService;
        _searchPagedQueryBuilder = searchPagedQueryBuilder;
    }

    public async Task<OperationResult> CreateAnnouncement(CreateAnnouncementModel createAnnouncementModel)
    {
        var userId = _userService.GetCurrentUserId();

        if (string.IsNullOrWhiteSpace(userId))
        {
            return new OperationResult(new[] {"userId is not set in cookies"}, IsSucceed: true);
        }

        var announcement = createAnnouncementModel.ToEntity();
        announcement.User.ID = userId;
        announcement.CreatedDateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        await _announcementRepository.SaveAsync(announcement);

        return new OperationResult(new[] { "Announcement is created"}, IsSucceed: true);
    }

    public async Task<OperationResult> UpdateAnnouncementAsync(UpdateAnnouncementModel updateAnnouncementModel)
    {
        var userId = _userService.GetCurrentUserId();

        if (string.IsNullOrWhiteSpace(userId))
        {
            return new OperationResult(new[] {"userId is not set in cookies"}, IsSucceed: true);
        }
        
        var announcement = updateAnnouncementModel.ToEntity();
        announcement.User.ID = userId;
        announcement.LastUpdateDateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        await _announcementRepository.UpdateAsync(announcement);

        return new OperationResult(ResultMessages: new[] {"Announcement is updated"}, IsSucceed: true);
    }

    public async Task<OperationResult> PatchAnnouncementAsync(PatchAnnouncementModel patchAnnouncementModel)
    {
        var announcement = patchAnnouncementModel.ToEntity();
        announcement.LastUpdateDateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        await _announcementRepository.UpdateFieldsAsync(announcement);

        return new OperationResult(ResultMessages: new[] {"Success"}, IsSucceed: true);
    }

    public async Task<OperationResult> DeleteAnnouncementAsync(string id)
    {
        await _announcementRepository.DeleteByIdAsync(id);

        return new OperationResult(ResultMessages: new[] {"Success"}, IsSucceed: true);
    }

    public async Task<PagedResponse<AnnouncementModel>> SearchAnnouncement(
        SearchAnnouncementModel searchAnnouncementModel)
    {
        _searchPagedQueryBuilder.BuildSearchQuery(searchAnnouncementModel);
        var query = _searchPagedQueryBuilder.GetQuery();
        var announcements = await query.ExecuteAsync();
        
        var result = (IReadOnlyList<AnnouncementModel>)announcements.Results
            .Select(c => c.ToBusinessModel())
            .ToList();

        return new PagedResponse<AnnouncementModel>(
            announcements.TotalCount,
            announcements.PageCount,
            result
        );
    }
}