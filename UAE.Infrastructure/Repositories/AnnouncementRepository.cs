using MongoDB.Entities;
using UAE.Core.Entities;
using UAE.Core.Repositories;
using UAE.Infrastructure.Repositories.Base.Implementation;

namespace UAE.Infrastructure.Repositories;

public class AnnouncementRepository :  RepositoryBase<Announcement>, IAnnouncementRepository
{
    public async Task UpdateFieldsAsync(Announcement announcement)
    {
        var updateCommand = DB.Update<Announcement>()
            .MatchID(announcement.ID);

        if (!string.IsNullOrWhiteSpace(announcement.Address))
        {
            updateCommand.Modify(a => a.Set(an => an.Address, announcement.Address));
        }
        
        if (!string.IsNullOrWhiteSpace(announcement.Description))
        {
            updateCommand.Modify(a => a.Set(an => an.Description, announcement.Description));
        }
        
        if (!string.IsNullOrWhiteSpace(announcement.AddressToTake))
        {
            updateCommand.Modify(a => a.Set(an => an.AddressToTake, announcement.AddressToTake));
        }
        
        if (!string.IsNullOrWhiteSpace(announcement.Category.ID))
        {
            updateCommand.Modify(a => a.Set(an => an.Category, announcement.Category.ID));
        }
        
        if (!string.IsNullOrWhiteSpace(announcement.User.ID))
        {
            updateCommand.Modify(a => a.Set(an => an.User, announcement.User.ID));
        }

        if (announcement.CategoryPath.Length > 0)
        {
            updateCommand.Modify(a => a.Set(an => an.CategoryPath, announcement.CategoryPath));
        }
        
        updateCommand.Modify(a => a.Set(an => an.LastUpdateDateTime, announcement.LastUpdateDateTime));

        foreach (var field in announcement.Fields.Names)
        {
            var fieldValue = announcement.Fields[field];
            updateCommand.Modify(a => a.Set(an => an.Fields![field], fieldValue));
        }

        foreach (var announcementPhoto in announcement.Pictures)
        {
            updateCommand.Modify(a => a.AddToSet(f => f.Pictures, announcementPhoto));
        }

        if (!string.IsNullOrWhiteSpace(announcement.Currency.ID))
        {
            updateCommand.Modify(a => a.Set(an => an.Currency, announcement.Currency.ID));
        }

        if (announcement.Price != null)
        {
            updateCommand.Modify(a => a.Set(f => f.Price, announcement.Price));
        }

        await updateCommand.ExecuteAsync();
    }
}