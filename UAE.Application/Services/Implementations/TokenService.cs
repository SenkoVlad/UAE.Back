using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UAE.Application.Models.User;
using UAE.Application.Services.Interfaces;
using UAE.Shared.Settings;

namespace UAE.Application.Services.Implementations;

public class TokenService : ITokenService
{
    private readonly IOptions<Settings> _settings;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public TokenService(IOptions<Settings> settings,
        IHttpContextAccessor httpContextAccessor
        )
    {
        _settings = settings;
        _httpContextAccessor = httpContextAccessor;
    }

    public bool IsUserLoggedAndTokenValid(LoginUserModel loginUserModel)
    {
        var token = GetRequestToken();

        if (string.IsNullOrWhiteSpace(token))
        {
            return false;
        }
        
        var validationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,
            ValidAudience = _settings.Value.Jwt.Issuer,
            ValidIssuer = _settings.Value.Jwt.Issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Value.Jwt.SecretKey))
        };

        var principal = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out _);
        var emailClaim = principal.Claims.FirstOrDefault(s => s.Type == ClaimTypes.Email)?.Value;
            
        return !string.IsNullOrWhiteSpace(emailClaim) && 
               emailClaim == loginUserModel.Email;
    }

    private string GetRequestToken()
    {
        var authBearerString = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
        var token = string.IsNullOrWhiteSpace(authBearerString)
            ? string.Empty
            : authBearerString.Split(" ").Last();
        
        return token;
    }
}