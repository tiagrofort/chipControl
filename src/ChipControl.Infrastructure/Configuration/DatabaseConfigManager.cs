namespace ChipControl.Infrastructure.Configuration;

using Microsoft.Extensions.Configuration;

public static class DatabaseConfigManager
{
    public static DatabaseConfig Load(string configPath)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(System.IO.Path.GetDirectoryName(configPath) ?? ".")
            .AddJsonFile(System.IO.Path.GetFileName(configPath), optional: false, reloadOnChange: true);

        var config = builder.Build();
        var dbConfig = new DatabaseConfig();
        config.GetSection("Database").Bind(dbConfig);

        return dbConfig;
    }

    public static void Save(string configPath, DatabaseConfig dbConfig)
    {
        var directory = System.IO.Path.GetDirectoryName(configPath) ?? ".";
        if (!System.IO.Directory.Exists(directory))
            System.IO.Directory.CreateDirectory(directory);

        var json = System.Text.Json.JsonSerializer.Serialize(
            new { Database = dbConfig },
            new System.Text.Json.JsonSerializerOptions { WriteIndented = true });

        System.IO.File.WriteAllText(configPath, json);
    }
}
