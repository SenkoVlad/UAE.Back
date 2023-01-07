namespace UAE.Application.Models.Category;

public sealed record FieldModel(
    string Name,
    int type,
    string[]? PossibleValues = null);