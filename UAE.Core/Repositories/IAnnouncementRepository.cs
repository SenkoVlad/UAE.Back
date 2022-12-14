using UAE.Core.Entities;
using UAE.Core.Repositories.Base;

namespace UAE.Core.Repositories;

public interface IAnnouncementRepository : IRepositoryBase<Announcement>
{
    Task UpdateFieldsAsync(Announcement announcement);

    Task<List<Announcement>> GetByIdsAsync(IEnumerable<string> announcementIds);
}