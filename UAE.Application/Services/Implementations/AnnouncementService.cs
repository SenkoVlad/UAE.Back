using MongoDB.Bson;
using MongoDB.Entities;
using UAE.Application.Extensions;
using UAE.Application.Mapper.Profiles;
using UAE.Application.Models;
using UAE.Application.Models.Announcement;
using UAE.Application.Services.Interfaces;
using UAE.Core.Entities;
using UAE.Core.EntityDataParameters;
using UAE.Core.Repositories;
using UAE.Shared;
using UAE.Shared.Extensions;

namespace UAE.Application.Services.Implementations;

internal sealed class AnnouncementService : IAnnouncementService
{
    private readonly IAnnouncementRepository _announcementRepository;
    private readonly IUserService _userService;
    
    public AnnouncementService(IAnnouncementRepository announcementRepository, 
        IUserService userService)
    {
        _announcementRepository = announcementRepository;
        _userService = userService;
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
        var query = DB.PagedSearch<Announcement>();
        
        if (!string.IsNullOrWhiteSpace(searchAnnouncementModel.CategoryId))
        {
            query.Match(a => a.Category.ID == searchAnnouncementModel.CategoryId);
        }

        foreach (var field in searchAnnouncementModel.Filters)
        {
            query.Match(a => a.Fields[field.Name] == field.Value);
        }

        if (!string.IsNullOrWhiteSpace(searchAnnouncementModel.Description))
        {
            query.Match( a=> a.Description.Contains(searchAnnouncementModel.Description));
        }
        
        query.Sort(a => a.Ascending(searchAnnouncementModel.SortedBy));

        query.PageSize(searchAnnouncementModel.PageSize)
             .PageNumber(searchAnnouncementModel.PageNumber);
        
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