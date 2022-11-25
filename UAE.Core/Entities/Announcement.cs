using MongoDB.Entities;

namespace UAE.Core.Entities;

public class Announcement : Entity
{
    public string Description { get; set; }
    
    public string Title { get; set; }

    public One<Category> Category { get; set; }
    
    public DateTime CreatedDateTime { get; set; }

    public Dictionary<string, string> Parameters { get; set; }
    
    public One<User> User { get; set; }

    public string AddressToTake { get; set; }

    public string Address { get; set; }
}