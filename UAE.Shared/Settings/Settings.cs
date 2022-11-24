namespace UAE.Shared.Settings;

public sealed record Settings
{
    public DatabaseSettings Database { get; set; }
    
    public JwtSettings Jwt { get; set; }
}