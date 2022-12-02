using System.Linq.Expressions;
using MongoDB.Entities;
using UAE.Application.Mapper;
using UAE.Application.Models;
using UAE.Application.Models.Announcement;
using UAE.Application.Services.Interfaces;
using UAE.Core.Entities;
using UAE.Core.Repositories;
using UAE.Shared;

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
        
        var announcement = ApplicationMapper.Mapper.Map<Announcement>(createAnnouncementModel);
        announcement.User.ID = userId;
        announcement.CreatedDateTime = DateTime.UtcNow;
        
        await _announcementRepository.SaveAsync(announcement);

        return new OperationResult(new[] { "Announcement is created"}, IsSucceed: true);
    }

    public async Task<OperationResult> UpdateAnnouncementAsync(UpdateAnnouncementModel updateAnnouncementModel)
    {
        var fieldsToUpdate = BuildFieldsToUpdateExpression(updateAnnouncementModel);
        await _announcementRepository.UpdateFieldsAsync(updateAnnouncementModel.EntityId, fieldsToUpdate);

        return new OperationResult(ResultMessages: new[] {"Announcement is updated"}, IsSucceed: true);
    }

    private Dictionary<Expression<Func<Announcement,object>>,object> BuildFieldsToUpdateExpression(UpdateAnnouncementModel updateAnnouncementModel)
    {
        var fieldsToUpdates = new Dictionary<Expression<Func<Announcement, object>>, object>();
        foreach (var field in updateAnnouncementModel.FieldsValuesToUpdate.Keys)
        {
            var propertyToUpdate = typeof(Announcement).GetProperty(field)!;
            var newFiledValue = updateAnnouncementModel.FieldsValuesToUpdate[field];
            fieldsToUpdates.Add(f => propertyToUpdate, newFiledValue);
        }

        return fieldsToUpdates;
    }

    public Task DeleteAnnouncementAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<AnnouncementModel> GetAnnouncementByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<PagedResponse<AnnouncementModel>> SearchAnnouncement(
        SearchAnnouncementModel searchAnnouncementModel)
    {
        var query = DB.PagedSearch<Announcement>();
        
        if (!string.IsNullOrWhiteSpace(searchAnnouncementModel.CategoryId))
        {
            query.Match(a => a.Category.ID == searchAnnouncementModel.CategoryId);
        }

        if (!string.IsNullOrWhiteSpace(searchAnnouncementModel.Description))
        {
            query.Match( a=> a.Description.Contains(searchAnnouncementModel.Description));
        }
        
        query.Sort(a => a.Ascending(searchAnnouncementModel.SortedBy));

        query.PageSize(searchAnnouncementModel.PageSize)
             .PageNumber(searchAnnouncementModel.PageNumber);
        
        var announcements = await query.ExecuteAsync();

        var result = announcements.Results.Select(x => new AnnouncementModel(x.Title, x.Description, x.CreatedDateTime.Ticks/10000)).ToList();

        //var result = ApplicationMapper.Mapper.Map<IReadOnlyList<AnnouncementModel>>(announcements.Results);

        return new PagedResponse<AnnouncementModel>(
            announcements.TotalCount,
            announcements.PageCount,
            result
        );
    }
}