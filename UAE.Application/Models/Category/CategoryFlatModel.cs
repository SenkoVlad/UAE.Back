namespace UAE.Application.Models.Category;

public sealed record CategoryFlatModel(
    string Id,
    string Label,
    Dictionary<string, Dictionary<string, object>> Fields);