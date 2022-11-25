using System.Threading.Tasks;
using UAE.Application.Models.User;

namespace UAE.Application.Services.Interfaces;

public interface IUserService
{
    Task RegisterAsync(CreateUserModel createUserModel);

    Task<LoginUserResult> LoginAsync(LoginUserModel loginUserModel);
}