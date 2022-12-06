using System.Text;
using UAE.Core.Entities;
using UAE.Infrastructure.Repositories.Base.Interfaces;

namespace UAE.Infrastructure.Repositories.Base.Implementation;

public class CommandBuilder : ICommandBuilder
{
    public string BuildUpdateAnnouncementStatement(Announcement announcement)
    {
        var stateStringBuilder = new StringBuilder();

        if (!string.IsNullOrWhiteSpace(announcement.Address))
        {
            stateStringBuilder.Append($"\"{nameof(announcement.Address)}\" : \"{announcement.Address}\",");
        }
        
        if (!string.IsNullOrWhiteSpace(announcement.Description))
        {
            stateStringBuilder.Append($"\"{nameof(announcement.Description)}\" : \"{announcement.Description}\",");
        }
        
        if (!string.IsNullOrWhiteSpace(announcement.AddressToTake))
        {
            stateStringBuilder.Append($"\"{nameof(announcement.AddressToTake)}\" : \"{announcement.AddressToTake}\",");
        }
        
        if (!string.IsNullOrWhiteSpace(announcement.Category.ID))
        {
            stateStringBuilder.Append($"\"{nameof(announcement.Category)}\" : \"{announcement.Category.ID}\",");
        }
        
        if (!string.IsNullOrWhiteSpace(announcement.User.ID))
        {
            stateStringBuilder.Append($"\"{nameof(announcement.User)}\" : \"{announcement.User.ID}\",");
        }

        stateStringBuilder.Append($"\"{nameof(announcement.LastUpdateDateTime)}\" : \"{announcement.LastUpdateDateTime}\",");

        if (announcement.Fields != null)
        {
            foreach (var field in announcement.Fields.Keys)
            {
                var fieldValue = announcement.Fields[field];

                switch (fieldValue.GetType().Name)
                {
                    case "Int32" :
                        stateStringBuilder.Append($"\"{nameof(announcement.Fields)}.{field}\" : {announcement.Fields[field]},");
                        break;
                    default:
                        stateStringBuilder.Append($"\"{nameof(announcement.Fields)}.{field}\" : \"{announcement.Fields[field]}\",");
                        break;
                }
            }
        }

        var fieldsToUpdate = stateStringBuilder.ToString().Remove(stateStringBuilder.Length - 1, 1);
        var state = $"{{$set : {{{fieldsToUpdate}}}}}";
        
        return state;
    }
}