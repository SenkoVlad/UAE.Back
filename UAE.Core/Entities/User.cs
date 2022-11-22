using MongoDB.Entities;

namespace UAE.Core.Entities;

public class User : Entity
{
    public string Name { get; set; }

    public Many<Announcement> Announcements { get; set; }
}