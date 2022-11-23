namespace UAE.Shared.Settings;

public sealed class Settings
{
    public DatabaseSettings Database { get; set; }
    
    public JwtSettings Jwt { get; set; }
}