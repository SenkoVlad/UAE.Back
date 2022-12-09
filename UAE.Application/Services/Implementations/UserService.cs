using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using UAE.Application.Mapper.Profiles;
using UAE.Application.Models.User;
using UAE.Application.Services.Interfaces;
using UAE.Core.Entities;
using UAE.Core.Repositories;

namespace UAE.Application.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public UserService(IUserRepository userRepository, 
        ITokenService tokenService, 
        IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task RegisterAsync(CreateUserModel createUserModel)
    {
        var (hash, salt) = CreateHashAndSalt(createUserModel.Password);
        
        var user = createUserModel.ToEntity();
        user.PasswordHash = hash;
        user.PasswordSalt = salt;

        await _userRepository.AddAsync(user);
    }

    public async Task<LoginUserResult> LoginAsync(LoginUserModel loginUserModel)
    {
        var user = await _userRepository.GetByEmailAsync(loginUserModel.Email);

        if (user == null)
        {
            return LoginUserResult.InCorrectPasswordOrEmail();
        }

        var isPasswordCorrect = IsPasswordCorrect(user, loginUserModel.Password);

        if (!isPasswordCorrect)
        {
            return LoginUserResult.InCorrectPasswordOrEmail();
        }

        var token = _tokenService.CreateToken(user);

        user.RefreshToken = Guid.NewGuid().ToString();
        _tokenService.AddTokenCookiesToResponse(token, user);

        await _userRepository.SaveAsync(user);
        
        return LoginUserResult.Succeded(token);
    }

    public string GetCurrentUserId()
    {
        _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("X-UserId", out var userId);

        return userId;
    }

    private bool IsPasswordCorrect(User user, string password)
    {
        var correctPasswordSalt = Convert.FromBase64String(user.PasswordSalt);
       
        using var hmac = new HMACSHA512(correctPasswordSalt);
        var inputPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        var compHash = Convert.ToBase64String(inputPasswordHash);
        return compHash.Equals(user.PasswordHash);
    }

    private (string, string) CreateHashAndSalt(string password)
    {
        var hmac = new HMACSHA512();
        var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        var hashString = Convert.ToBase64String(hashBytes);
        var saltString = Convert.ToBase64String(hmac.Key);
        
        return (hashString, saltString);
    }
}