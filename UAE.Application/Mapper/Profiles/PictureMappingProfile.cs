using UAE.Application.Models.Picture;
using UAE.Core.Entities;

namespace UAE.Application.Mapper.Profiles;

public static class PictureMappingProfile
{
    public static PictureModel ToBusinessModel(this Picture picture)
    {
        return new PictureModel(picture.Path, picture.Name);
    }
}