using MongoDB.Bson;
using UAE.Core.EntityDataParameters;
using UAE.Shared.Enum;

namespace UAE.Core.Entities;

public sealed record Field(
    string Name,
    FieldType Type,
    string ValueType,
    FilterCriteria Criteria,
    BsonValue[]? PossibleValues = null);
