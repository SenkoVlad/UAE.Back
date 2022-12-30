using UAE.Core.Entities;

namespace UAE.Application.Models.Category;

public sealed record CategoryWithParentPathModel(
    Core.Entities.Category Category,
    List<Field> Fields,
    CategoryShortModel[] ChildrenCategories = null,
    CategoryShortModel[] ParentCategories = null);