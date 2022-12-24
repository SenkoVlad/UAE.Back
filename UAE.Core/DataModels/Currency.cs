using MongoDB.Entities;

namespace UAE.Core.DataModels;

public class Currency : Entity
{
    public string Name { get; set; }
}