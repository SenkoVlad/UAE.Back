namespace UAE.Application.Models.Category;

public sealed record CategoryModel(
    string Id,
    string Label,
    List<CategoryModel> Children,
    Dictionary<string, Dictionary<string, object>> Fields);
