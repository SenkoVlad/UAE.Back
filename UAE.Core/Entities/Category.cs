using MongoDB.Entities;

namespace UAE.Core.Entities;

public class Category : Entity
{
    public string Name { get; set; }

    public List<Category> ChildrenCategories { get; set; } = new();
}