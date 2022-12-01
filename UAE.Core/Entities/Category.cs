using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Entities;
using UAE.Core.EntityDataParameters;

namespace UAE.Core.Entities;

public class Category : Entity
{
    public string Label { get; set; }

    public List<Category> Children { get; set; } = new();
    
    // [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfDocuments)]
    public Dictionary<string, Dictionary<string, object>> Parameters { get; set; }
}