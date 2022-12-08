using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using UAE.Api.Logging;
using UAE.Api.Middlewares;

namespace UAE.Api.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddLoggerService(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerManager, LoggerManager>();
    }

    public static void AddCustomExceptionHandler(this IServiceCollection services)
    {
        services.AddScoped<ExceptionMiddleware>();
    }

    public static void AddJwtAuth(this IServiceCollection services, ConfigurationManager builderConfiguration)
    {
        services
            .AddAuthentication(i =>
            {
                i.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                i.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                i.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                i.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builderConfiguration["Settings:Jwt:Issuer"],
                    ValidAudience = builderConfiguration["Settings:Jwt:Issuer"],
                    IssuerSigningKey = new
                        SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes
                            (builderConfiguration["Settings:Jwt:SecretKey"]!))
                };

                options.SaveToken = true;
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.ContainsKey("X-Access-Token"))
                        {
                            context.Token = context.Request.Cookies["X-Access-Token"];
                        }
                    
                        return Task.CompletedTask;
                    }
                };
            })
            .AddCookie(options =>
            {
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.IsEssential = true;
            });
    }

    public static void AddAuthSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(options => {
            options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
                Name = "Authorization",
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });
            options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
                {
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
                        Reference = new Microsoft.OpenApi.Models.OpenApiReference {
                            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });
    }
}