using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Entities;
using UAE.Application.Mapper;
using UAE.Application.Models.Order;
using UAE.Application.Services.Interfaces;
using UAE.Core.Entities;
using UAE.Shared;

namespace UAE.Application.Services.Implementations;

internal sealed class AnnouncementService : IAnnouncementService
{
    public Task<AnnouncementModel> CreateAnnouncement(AnnouncementModel announcementModel)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAnnouncementAsync(AnnouncementModel announcement)
    {
        throw new NotImplementedException();
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
        var result = ApplicationMapper.Mapper.Map<IReadOnlyList<AnnouncementModel>>(announcements.Results);

        return new PagedResponse<AnnouncementModel>
        {
            PageCount = announcements.PageCount,
            TotalCount = announcements.TotalCount,
            Items = result
        };
    }
}