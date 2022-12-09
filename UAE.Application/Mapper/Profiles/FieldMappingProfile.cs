using UAE.Application.Models.Category;
using UAE.Core.Entities;

namespace UAE.Application.Mapper.Profiles;

public static class FieldMappingProfile
{
    public static Field ToEntity(this FieldModel fieldModel) => 
        new(fieldModel.Name, fieldModel.Type);
    
    public static FieldModel ToBusinessModel(this Field field) => 
        new(field.Name, field.Type);
}