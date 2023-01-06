using UAE.Application.Models.Category;
using UAE.Core.Entities;

namespace UAE.Application.Mapper.Profiles;

public static class CategoryMappingProfile
{
    public static CategoryModel ToBusinessModel(this Category entity)
    {
        try
        {
            return new CategoryModel(
                Children : entity.Children.Select(c => c.ToBusinessModel())
                    .ToList(),
                Fields : entity.Fields.Select(f => f.ToBusinessModel())
                    .ToList(),
                Label : entity.Label,
                Id : entity.ID
            );
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
}