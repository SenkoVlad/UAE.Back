using MongoDB.Entities;

namespace UAE.Core.Entities;

public class Category : Entity
{
    public string Label { get; set; }

    public List<Category> ChildrenCategories { get; set; } = new();
}