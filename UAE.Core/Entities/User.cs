using MongoDB.Entities;

namespace UAE.Core.Entities;

public class User : Entity
{
    public string Email { get; set; }

    public string PasswordHash { get; set; }
    
    public string PasswordSalt { get; set; }

    public DateTime LastLoginDateTime { get; set; }

    public Many<Announcement> Announcements { get; set; }

    public string RefreshToken { get; set; }
}