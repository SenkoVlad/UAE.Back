namespace UAE.Api.Settings;

public sealed class Settings
{
    public DatabaseSettings Database { get; set; }
    
    public JwtSettings Jwt { get; set; }
}