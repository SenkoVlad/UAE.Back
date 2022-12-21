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
    private readonly IFileService _fileService;
    
    public AnnouncementService(IAnnouncementRepository announcementRepository, 
        IUserService userService, 
        IPagedQueryBuilderService<Announcement> searchPagedQueryBuilder,
        IFileService fileService)
    {
        _announcementRepository = announcementRepository;
        _userService = userService;
        _searchPagedQueryBuilder = searchPagedQueryBuilder;
        _fileService = fileService;
    }

    public async Task<OperationResult<Announcement>> CreateAnnouncement(CreateAnnouncementModel createAnnouncementModel)
    {
        var userId = _userService.GetCurrentUserId();

        if (string.IsNullOrWhiteSpace(userId))
        {
            return new OperationResult<Announcement>(new[] {"UserId is not set in cookies"}, IsSucceed: true);
        }

        var announcementPictures = await _fileService.SavePictures(createAnnouncementModel.Pictures);
        var announcement = createAnnouncementModel.ToEntity();
        announcement.User.ID = userId;
        announcement.CreatedDateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        announcement.Photos = announcementPictures is {IsSucceed: true, Result: { }}
            ? announcementPictures.Result!
            : new List<Photo>();
        await _announcementRepository.SaveAsync(announcement);
        
        return new OperationResult<Announcement>(new[] { "Announcement is created"}, IsSucceed: true, Result: announcement);
    }

    public async Task<OperationResult<Announcement>> UpdateAnnouncementAsync(UpdateAnnouncementModel updateAnnouncementModel)
    {
        var userId = _userService.GetCurrentUserId();

        if (string.IsNullOrWhiteSpace(userId))
        {
            return new OperationResult<Announcement>(new[] {"userId is not set in cookies"}, IsSucceed: true);
        }
        
        var announcement = updateAnnouncementModel.ToEntity();
        announcement.User.ID = userId;
        announcement.LastUpdateDateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        await _announcementRepository.UpdateAsync(announcement);

        return new OperationResult<Announcement>(ResultMessages: new[] {"Announcement is updated"}, IsSucceed: true);
    }

    public async Task<OperationResult<Announcement>> PatchAnnouncementAsync(PatchAnnouncementModel patchAnnouncementModel)
    {
        var announcement = patchAnnouncementModel.ToEntity();

        if (patchAnnouncementModel.Pictures != null)
        {
            var announcementPictures = await _fileService.SavePictures(patchAnnouncementModel.Pictures);
            announcement.Photos = announcementPictures.IsSucceed
                ? announcementPictures.Result
                : new List<Photo>();
        }
        
        announcement.LastUpdateDateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        await _announcementRepository.UpdateFieldsAsync(announcement);

        return new OperationResult<Announcement>(ResultMessages: new[] {"Success"}, IsSucceed: true, Result: announcement);
    }

    public async Task<OperationResult<string>> DeleteAnnouncementAsync(string id)
    {
        await _announcementRepository.DeleteByIdAsync(id);

        return new OperationResult<string>(ResultMessages: new[] {"Success"}, IsSucceed: true);
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