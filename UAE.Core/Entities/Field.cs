using UAE.Shared.Enum;

namespace UAE.Core.Entities;

public sealed record Field(
    string Name,
    FieldType Type,
    FieldValueType ValueType,
    FilterCriteria Criteria,
    string[]? PossibleValues = null);
