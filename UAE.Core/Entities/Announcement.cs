using MongoDB.Bson;
using MongoDB.Entities;
using UAE.Core.DataModels;
using static System.String;

namespace UAE.Core.Entities;

public class Announcement : Entity
{
    public string Description { get; init; } = Empty;

    public string Title { get; set; } = Empty;

    public One<Category> Category { get; set; } = new();

    public CategoryPath[] CategoryPath { get; set; } = Array.Empty<CategoryPath>();
    
    public long CreatedDateTime { get; set; }
    
    public decimal Price { get; set; }
    
    public Currency Currency { get; set; }
    
    public long LastUpdateDateTime { get; set; }

    public BsonDocument Fields { get; set; } = new();

    public One<User> User { get; set; } = new();

    public string AddressToTake { get; set; } = Empty;

    public string Address { get; set; } = Empty;

    public Picture[] Pictures { get; set; } = Array.Empty<Picture>();
}