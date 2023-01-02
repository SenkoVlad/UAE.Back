using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using UAE.Application.Mapper.Profiles;
using UAE.Application.Models;
using UAE.Application.Models.User;
using UAE.Application.Services.Interfaces;
using UAE.Core.Entities;
using UAE.Core.Repositories;

namespace UAE.Application.Services.Implementations;

internal sealed class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAnnouncementRepository _announcementRepository;
    
    public UserService(IUserRepository userRepository, 
        ITokenService tokenService, 
        IHttpContextAccessor httpContextAccessor, 
        IAnnouncementRepository announcementRepository)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _httpContextAccessor = httpContextAccessor;
        _announcementRepository = announcementRepository;
    }

    public async Task<OperationResult<User>> RegisterAsync(CreateUserModel createUserModel)
    {
        var userWithTheSameEmail = await _userRepository.GetByEmailAsync(createUserModel.Email);

        if (userWithTheSameEmail != null)
        {
            return new OperationResult<User>(IsSucceed: false, ResultMessages: new[] {"User is already exists"});
        }
        
        var (hash, salt) = CreateHashAndSalt(createUserModel.Password);
        var user = createUserModel.ToEntity();
        user.PasswordHash = hash;
        user.PasswordSalt = salt;
        user.CreatedDateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        await _userRepository.AddAsync(user);

        return new OperationResult<User>(IsSucceed: true, ResultMessages: new[] {"User is registered"});
    }

    public async Task<OperationResult<string>> LoginAsync(LoginUserModel loginUserModel)
    {
        var user = await _userRepository.GetByEmailAsync(loginUserModel.Email);

        if (user == null)
        {
            return new OperationResult<string>
            (IsSucceed: false,
                 Result: string.Empty, ResultMessages: new []{"Incorrect password or email"});
        }

        var isPasswordCorrect = IsPasswordCorrect(user, loginUserModel.Password);

        if (!isPasswordCorrect)
        {
            return new OperationResult<string>
            (IsSucceed: false,
                Result: string.Empty, ResultMessages: new []{ "Incorrect password or email"});
        }

        var token = _tokenService.CreateToken(user);

        user.RefreshToken = Guid.NewGuid().ToString();
        user.LastLoginDateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        _tokenService.AddTokenCookiesToResponse(token, user);
        await _userRepository.SaveAsync(user);
                   
        return new OperationResult<string>
        (IsSucceed: false,
            Result: token, ResultMessages: new []{ "Success"});
    }

    public string GetCurrentUserId()
    {
        _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("X-UserId", out var userId);

        return userId;
    }

    public async Task<OperationResult<string>> LikeAnnouncementAsync(string announcementId)
    {
        _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("X-UserId", out var userId);

        if (string.IsNullOrWhiteSpace(userId))
        {
            return new OperationResult<string>(IsSucceed: false, ResultMessages: new[] {"there is not userId in cookies"});
        }
        
        var isAnnouncementExist = await _announcementRepository.IsExists(announcementId);
        if (!isAnnouncementExist)
        {
            return new OperationResult<string>(IsSucceed: false, ResultMessages: new[] {"announcement with id is not exists"});
        }

        var isAlreadyLiked = await _userRepository.IsAnnouncementAlreadyLiked(userId, announcementId);
        if (isAlreadyLiked)
        {
            return new OperationResult<string>(IsSucceed: false, ResultMessages: new[] {"announcement is already liked"});
        }
        
        var result = await _userRepository.LikeAnnouncementAsync(userId, announcementId);

        return new OperationResult<string>(IsSucceed: result);
    }

    public async Task<OperationResult<string>> UnLikeAnnouncementAsync(string announcementId)
    {
        _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("X-UserId", out var userId);

        if (string.IsNullOrWhiteSpace(userId))
        {
            return new OperationResult<string>(IsSucceed: false, ResultMessages: new[] {"there is not userId in cookies"});
        }

        var isAnnouncementExist = await _announcementRepository.IsExists(announcementId);
        if (!isAnnouncementExist)
        {
            return new OperationResult<string>(IsSucceed: false, ResultMessages: new[] {"announcement with id is not exists"});
        }
        
        var result = await _userRepository.UnLikeAnnouncement(userId, announcementId);

        return new OperationResult<string>(IsSucceed: result);
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