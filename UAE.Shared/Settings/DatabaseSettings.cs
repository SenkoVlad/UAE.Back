namespace UAE.Shared.Settings;

public sealed record DatabaseSettings
{
    public string Host { get; set; }
    
    public int Port { get; set; }
    
    public string Name { get; set; }
    
    public string Username { get; set; }
    
    public string Password { get; set; }
}
