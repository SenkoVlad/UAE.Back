using MongoDB.Bson;
using MongoDB.Entities;

namespace UAE.Core.Entities;

public class Announcement : Entity
{
    public string Description { get; set; }
    
    public string Title { get; set; }

    public One<Category> Category { get; set; } = new();
    
    public long CreatedDateTime { get; set; }
    
    public long LastUpdateDateTime { get; set; }

    public BsonDocument Fields { get; set; }

    public One<User> User { get; set; } = new();

    public string AddressToTake { get; set; }

    public string Address { get; set; }

    public string FieldsName => 
        nameof(Fields);
}