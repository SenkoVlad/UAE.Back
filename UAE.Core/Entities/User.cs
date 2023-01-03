using MongoDB.Entities;
using UAE.Core.DataModels;

namespace UAE.Core.Entities;

public class User : Entity
{
    public string Email { get; set; }

    public string PasswordHash { get; set; }
    
    public string PasswordSalt { get; set; }

    public long LastLoginDateTime { get; set; }
    
    public long CreatedDateTime { get; set; }

    public Many<Announcement> Announcements { get; set; }

    public string[] Likes { get; set; } = Array.Empty<string>();
    
    public AnnouncementBrowsingHistory[] BrowsingHistories { get; set; } = Array.Empty<AnnouncementBrowsingHistory>();

    public string RefreshToken { get; set; }
}