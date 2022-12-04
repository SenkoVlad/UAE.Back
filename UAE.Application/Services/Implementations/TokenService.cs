using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UAE.Application.Models;
using UAE.Application.Models.User;
using UAE.Application.Services.Interfaces;
using UAE.Core.Entities;
using UAE.Core.Repositories;
using UAE.Shared.Settings;

namespace UAE.Application.Services.Implementations;

public class TokenService : ITokenService
{
    private readonly IOptions<Settings> _settings;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRepository _userRepository;

    public TokenService(IOptions<Settings> settings,
        IHttpContextAccessor httpContextAccessor,
        IUserRepository userRepository)
    {
        _settings = settings;
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
    }

    public async Task<OperationResult> RefreshAsync()
    {
        var (userEmail, refreshToken) = GetUserEmailAndRefreshTokenFromCookies();

        if (string.IsNullOrWhiteSpace(userEmail) || string.IsNullOrWhiteSpace(refreshToken))
        {
            return new OperationResult(new[] {"User Email cookie or refresh token are missing"}, IsSucceed: false); 
        }

        var user = await _userRepository.GetByQuery(u => u.Email == userEmail && u.RefreshToken == refreshToken);

        if (user == null)
        {
            return new OperationResult(new[] { "User Email cookie or refresh token are incorrect"}, IsSucceed: false); 
        }

        var token = CreateToken(user);
        user.RefreshToken = Guid.NewGuid().ToString();
        AddTokenCookiesToResponse(token, user);
        
        await _userRepository.SaveAsync(user);
        
        return new OperationResult(new[] {"Token and refresh tokens are updated"}, IsSucceed: true);
    }

    public string CreateToken(User user)
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
            expires: DateTime.UtcNow.AddDays(10),
            signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    public void AddTokenCookiesToResponse(string token, User user)
    {
        _httpContextAccessor.HttpContext.Response.Cookies.Append("X-Access-Token", token,
            new CookieOptions {HttpOnly = true, SameSite = SameSiteMode.Strict});
        _httpContextAccessor.HttpContext.Response.Cookies.Append("X-Username", user.Email,
            new CookieOptions {HttpOnly = true, SameSite = SameSiteMode.Strict});
        _httpContextAccessor.HttpContext.Response.Cookies.Append("X-UserId", user.ID,
            new CookieOptions {HttpOnly = true, SameSite = SameSiteMode.Strict});
        _httpContextAccessor.HttpContext.Response.Cookies.Append("X-Refresh-Token", user.RefreshToken,
            new CookieOptions {HttpOnly = true, SameSite = SameSiteMode.Strict});
    }

    private (string userEmail, string refreshToken) GetUserEmailAndRefreshTokenFromCookies()
    {
        var userEmail = _httpContextAccessor.HttpContext.Request.Cookies["X-Username"];
        var refreshToken = _httpContextAccessor.HttpContext.Request.Cookies["X-Refresh-Token"];

        return (userEmail, refreshToken);
    }
}