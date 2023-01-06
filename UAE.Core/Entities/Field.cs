using MongoDB.Bson;

namespace UAE.Core.Entities;

public sealed record Field(
    string Name,
    string FieldType,
    string ValueType,
    string Criteria,
    BsonValue[]? PossibleValues = null);
