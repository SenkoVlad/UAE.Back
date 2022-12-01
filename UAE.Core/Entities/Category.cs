using MongoDB.Entities;

namespace UAE.Core.Entities;

public class Category : Entity
{
    public string Label { get; set; }

    public List<Category> Children { get; set; } = new();
    
    public Dictionary<string, Dictionary<string, object>> Fields { get; set; }
}