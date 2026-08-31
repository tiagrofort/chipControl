namespace ChipControl.Infrastructure.Configuration;

public static class DatabaseConfigPaths
{
    public static string GetConfigPath()
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var appFolder = Path.Combine(appData, "ChipControl");
        Directory.CreateDirectory(appFolder);
        return Path.Combine(appFolder, "database.json");
    }

    public static string GetDefaultSqlitePath()
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var appFolder = Path.Combine(appData, "ChipControl");
        Directory.CreateDirectory(appFolder);
        return Path.Combine(appFolder, "chipcontrol.db");
    }

    public static bool ConfigExists()
    {
        var path = GetConfigPath();
        return File.Exists(path);
    }
}
