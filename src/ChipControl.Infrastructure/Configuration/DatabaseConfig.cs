namespace ChipControl.Infrastructure.Configuration;

public class DatabaseConfig
{
    public string Provider { get; set; } = "SQLite";
    public string ConnectionString { get; set; } = string.Empty;
    public string? Database { get; set; }
    public string? Server { get; set; }
    public int? Port { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
}
