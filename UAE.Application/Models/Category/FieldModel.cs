namespace UAE.Application.Models.Category;

public sealed record FieldModel(
    string Name,
    string FieldType,
    object[]? PossibleValues = null);