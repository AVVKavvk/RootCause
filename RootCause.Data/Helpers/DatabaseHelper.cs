namespace RootCause.Data.Helpers;

public static class DatabaseHelper
{
    public static string GetConnectionString()
    {
        var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var dbFolder = Path.Combine(home, ".RootCauseData");
        Directory.CreateDirectory(dbFolder);
        var dbPath = Path.Combine(dbFolder, "RootCauseData.db3");
        return $"Data Source={dbPath}";
    }
}
