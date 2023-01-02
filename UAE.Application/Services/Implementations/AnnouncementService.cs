using UAE.Application.Mapper.Profiles;
using UAE.Application.Models;
using UAE.Application.Models.Announcement;
using UAE.Application.Services.Interfaces;
using UAE.Application.Services.Interfaces.Base;
using UAE.Core.DataModels;
using UAE.Core.Entities;
using UAE.Core.Repositories;
using UAE.Shared;

namespace UAE.Application.Services.Implementations;

internal sealed class AnnouncementService : IAnnouncementService
{
    private readonly IAnnouncementRepository _announcementRepository;
    private readonly IUserService _userService;
    private readonly IPagedQueryBuilderService<Announcement> _searchPagedQueryBuilder;
    private readonly IFileService _fileService;
    private readonly ICategoryInMemory _categoryInMemory;
    private readonly IInMemoryService<Currency> _currencyInMemory;
    
    public AnnouncementService(IAnnouncementRepository announcementRepository, 
        IUserService userService, 
        IPagedQueryBuilderService<Announcement> searchPagedQueryBuilder,
        IFileService fileService, ICategoryInMemory categoryInMemory, 
        IInMemoryService<Currency> currencyInMemory)
    {
        _announcementRepository = announcementRepository;
        _userService = userService;
        _searchPagedQueryBuilder = searchPagedQueryBuilder;
        _fileService = fileService;
        _categoryInMemory = categoryInMemory;
        _currencyInMemory = currencyInMemory;
    }

    public async Task<OperationResult<Announcement>> CreateAnnouncement(CreateAnnouncementModel createAnnouncementModel)
    {
        var userId = _userService.GetCurrentUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return new OperationResult<Announcement>(IsSucceed: true, ResultMessages: new[] {"UserId is not set in cookies"});
        }
        
        var currency = _currencyInMemory.Data.FirstOrDefault(c => c.Code == createAnnouncementModel.CurrencyCode);
        if (currency == null)
        {
            return new OperationResult<Announcement>(IsSucceed: false, ResultMessages: new[] { "Announcement is not created. Currency is not correct"});
        }

        var announcementPictures = await _fileService.SavePictures(createAnnouncementModel.Pictures);
        
        var announcement = createAnnouncementModel.ToEntity();
        announcement.Pictures = announcementPictures is {IsSucceed: true, Result: { }}
            ? announcementPictures.Result
            : Array.Empty<Picture>();
        announcement.Currency = currency;
        announcement.User.ID = userId;
        announcement.CreatedDateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        announcement.CategoryPath = _categoryInMemory.GetCategoryPath(createAnnouncementModel.CategoryId);

        await _announcementRepository.SaveAsync(announcement);
        
        return new OperationResult<Announcement>(IsSucceed: true, Result: announcement, ResultMessages: new[] { "Announcement is created"});
    }

    public async Task<OperationResult<Announcement>> UpdateAnnouncementAsync(UpdateAnnouncementModel updateAnnouncementModel)
    {
        var userId = _userService.GetCurrentUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return new OperationResult<Announcement>(IsSucceed: true, ResultMessages: new[] {"userId is not set in cookies"});
        }
        
        var currency = _currencyInMemory.Data.FirstOrDefault(c => c.Code == updateAnnouncementModel.CurrencyCode);
        if (currency == null)
        {
            return new OperationResult<Announcement>(IsSucceed: false, ResultMessages: new[] { "Announcement is not updated. Currency is not correct"});
        }
        
        var announcement = updateAnnouncementModel.ToEntity();

        if (updateAnnouncementModel.Pictures is {Count: > 0})
        {
            var announcementPictures = await _fileService.SavePictures(updateAnnouncementModel.Pictures);
            announcement.Pictures = announcementPictures.IsSucceed
                ? announcementPictures.Result
                : Array.Empty<Picture>();
        }
        
        announcement.User.ID = userId;
        announcement.Currency = currency;
        announcement.LastUpdateDateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        announcement.CategoryPath = _categoryInMemory.GetCategoryPath(updateAnnouncementModel.CategoryId);
        
        await _announcementRepository.UpdateAsync(announcement);

        return new OperationResult<Announcement>(IsSucceed: true, Result: announcement, ResultMessages: new[] {"Announcement is updated"});
    }

    public async Task<OperationResult<Announcement>> PatchAnnouncementAsync(PatchAnnouncementModel patchAnnouncementModel)
    {
        var announcement = patchAnnouncementModel.ToEntity();

        if (!string.IsNullOrWhiteSpace(announcement.Currency.Code))
        {
            var currency = _currencyInMemory.Data.FirstOrDefault(c => c.Code == patchAnnouncementModel.CurrencyCode);
            if (currency == null)
            {
                return new OperationResult<Announcement>(IsSucceed: false, ResultMessages: new[] { "Announcement is not patched. Currency is not correct"});
            }
        }

        if (patchAnnouncementModel.Pictures != null)
        {
            var announcementPictures = await _fileService.SavePictures(patchAnnouncementModel.Pictures);
            announcement.Pictures = announcementPictures.IsSucceed
                ? announcementPictures.Result
                : Array.Empty<Picture>();
        }

        if (!string.IsNullOrWhiteSpace(patchAnnouncementModel.CategoryId))
        {
            announcement.CategoryPath =  _categoryInMemory.GetCategoryPath(patchAnnouncementModel.CategoryId);
        }
        
        announcement.LastUpdateDateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        await _announcementRepository.UpdateFieldsAsync(announcement);

        return new OperationResult<Announcement>(IsSucceed: true, Result: announcement, ResultMessages: new[] {"Success"});
    }

    public async Task<OperationResult<string>> DeleteAnnouncementAsync(string id)
    {
        await _announcementRepository.DeleteByIdAsync(id);

        return new OperationResult<string>(IsSucceed: true, ResultMessages: new[] {"Success"});
    }

    public async Task<PagedResponse<AnnouncementModel>> SearchAnnouncement(
        SearchAnnouncementModel searchAnnouncementModel)
    {
        if (searchAnnouncementModel.CategoryIds.Any())
        {
            var categoryChildren = _categoryInMemory.GetChildrenCategories(searchAnnouncementModel.CategoryIds);
            searchAnnouncementModel.CategoryIds.AddRange(categoryChildren);
        }

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