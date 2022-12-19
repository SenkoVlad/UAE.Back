using MongoDB.Entities;
using UAE.Application.Models.Announcement;

namespace UAE.Application.Services.Interfaces;

public interface IPagedQueryBuilderService<T> where T : IEntity
{
    void BuildSearchQuery(SearchAnnouncementModel searchAnnouncementModel);
    
    PagedSearch<T> GetQuery();
}