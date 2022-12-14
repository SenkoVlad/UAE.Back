using MongoDB.Entities;

namespace UAE.Core.Entities;

public class Category : Entity
{
    public string Label { get; set; }

    public string Name { get; set; }

    public List<Category> Children { get; set; } = new();

    public List<Field> Fields { get; set; } = new();
}