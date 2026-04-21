using Microsoft.Data.Sqlite;

namespace RootCause.Data.Context;

public class BugDBContext
{
    private readonly string _connectionString;

    public BugDBContext()
    {
        var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var dbFolder = Path.Combine(home, ".RootCauseData");
        Directory.CreateDirectory(dbFolder);

        var dbPath = Path.Combine(dbFolder, "RootCauseData.db3");
        _connectionString = $"Data Source={dbPath}";

        InitialiseDatabase();
    }

    private void InitialiseDatabase()
    {
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();

        var cmd = conn.CreateCommand();

        cmd.CommandText = """
            CREATE TABLE IF NOT EXISTS Bugs (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Title TEXT NOT NULL,
                ErrorMessage TEXT NOT NULL,
                RootCause TEXT NOT NULL,
                Fix TEXT NOT NULL,
                TimeToSolve INTEGER NOT NULL,
                StackTags TEXT NOT NULL,
                ResolvedAt DATE,
                Serverity INTEGER NOT NULL,
                CreatedAt DATE NOT NULL
            );
            """;
        cmd.ExecuteNonQuery();
    }

    public SqliteConnection? GetConnection()
    {
        try
        {
            return new SqliteConnection(_connectionString);
        }
        catch
        {
            return null;
        }
    }
}
