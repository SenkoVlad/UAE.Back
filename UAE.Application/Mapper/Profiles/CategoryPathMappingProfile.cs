using UAE.Application.Models.Category;
using UAE.Core.Entities;

namespace UAE.Application.Mapper.Profiles;

public static class CategoryPathMappingProfile
{
    public static CategoryPath ToEntity(this CategoryShortModel model)
    {
        return new CategoryPath
        {
            ID = model.Id,
            Label = model.Label
        };
    }
    
    public static CategoryShortModel ToBusinessModel(this CategoryPath entity)
    {
        return new CategoryShortModel(entity.ID, entity.Label);
    }
}