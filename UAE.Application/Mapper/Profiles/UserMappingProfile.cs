using UAE.Application.Models.User;
using UAE.Core.Entities;

namespace UAE.Application.Mapper.Profiles;

public static class UserMappingProfile
{
    public static User ToEntity(this CreateUserModel model)
    {
        return new User
        {
            Email = model.Email
        };
    }    
}