using MongoDB.Entities;
using UAE.Core.EntityDataParameters;

namespace UAE.Core.Entities;

public class Announcement : Entity
{
    public string Description { get; set; }
    
    public string Title { get; set; }

    public One<Category> Category { get; set; } = new();
    
    public DateTime CreatedDateTime { get; set; }

    public Dictionary<string, object> Fields { get; set; }

    public One<User> User { get; set; } = new();

    public string AddressToTake { get; set; }

    public string Address { get; set; }
}