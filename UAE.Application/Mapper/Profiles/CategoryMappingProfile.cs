using UAE.Application.Models.Category;
using UAE.Core.Entities;

namespace UAE.Application.Mapper.Profiles;

public static class CategoryMappingProfile
{
    public static Category ToEntity(this CategoryModel model)
    {
        return new Category
        {
            Children = model.Children.Select(c => c.ToEntity())
                .ToList(),
            Fields = model.Fields,
            Label = model.Label,
            ID = model.Id
        };
    }
    
    public static CategoryModel ToBusinessModel(this Category entity)
    {
        return new CategoryModel(
            Children : entity.Children.Select(c => c.ToBusinessModel())
                .ToList(),
            Fields : entity.Fields,
            Label : entity.Label,
            Id : entity.ID
        );
    }
}