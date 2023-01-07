namespace UAE.Application.Models.Category;

public sealed record FieldModel(
    string Name,
    int Type,
    string[]? PossibleValues = null);