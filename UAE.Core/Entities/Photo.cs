using MongoDB.Entities;

namespace UAE.Core.Entities;

public class Photo : Entity
{
    public string Path { get; set; }

    public string Name { get; set; }
}