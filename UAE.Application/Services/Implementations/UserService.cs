using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Entities;
using UAE.Application.Mapper;
using UAE.Application.Models.User;
using UAE.Application.Services.Interfaces;
using UAE.Core.Entities;
using UAE.Core.Repositories;
using UAE.Shared.Settings;

namespace UAE.Application.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IOptions<Settings> _settings;
    private readonly ITokenService _tokenService;
    
    public UserService(IUserRepository userRepository, 
        IOptions<Settings> settings, 
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _settings = settings;
        _tokenService = tokenService;
    }

    public async Task RegisterAsync(CreateUserModel createUserModel)
    {
        var (hash, salt) = CreateHashAndSalt(createUserModel.Password);
        
        var user = ApplicationMapper.Mapper.Map<User>(createUserModel);
        user.PasswordHash = hash;
        user.PasswordSalt = salt;

        await _userRepository.AddAsync(user);
    }

    public async Task<LoginUserResult> LoginAsync(LoginUserModel loginUserModel)
    {
        var isUserAlreadyLogged = _tokenService.IsUserLoggedAndTokenValid(loginUserModel);

        if (isUserAlreadyLogged)
        {
            return LoginUserResult.AlreadyLogged();
        }
        
        var user = await _userRepository.GetByEmailAsync(loginUserModel.Email);

        if (user == null)
        {
            return LoginUserResult.AlreadyLogged();
        }

        var isPasswordCorrect = IsPasswordCorrect(user, loginUserModel.Password);

        if (!isPasswordCorrect)
        {
            return LoginUserResult.InCorrectPasswordOrEmail();
        }

        var token = CreateToken(user);

        return LoginUserResult.Succeded(token);
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email),
        };
        
        var secretKeyBytes = Encoding.UTF8.GetBytes(_settings.Value.Jwt.SecretKey);
        var key = new SymmetricSecurityKey(secretKeyBytes);

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            audience: _settings.Value.Jwt.Issuer,
            issuer: _settings.Value.Jwt.Issuer,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
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