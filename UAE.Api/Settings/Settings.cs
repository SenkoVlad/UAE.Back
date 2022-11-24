namespace UAE.Api.Settings;

public sealed record Settings
{
    public DatabaseSettings Database { get; set; }
}