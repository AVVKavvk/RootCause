using RootCause.Core.Entities;
using RootCause.Core.Enums;
using RootCause.Core.Interfaces;
using RootCause.Data.Context;

namespace RootCause.Data.Repositories;

public class BugRepository : IBugRepository
{
    private readonly BugDBContext _context;

    public BugRepository()
    {
        _context = new BugDBContext();
    }

    public async Task<List<Bug>> GetBugsAsync()
    {
        try
        {
            using var conn = _context.GetConnection();

            if (conn is null)
            {
                throw new Exception("No Connection");
            }
            conn.Open();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM Bugs";

            var reader = await cmd.ExecuteReaderAsync();

            var bugs = new List<Bug>();

            while (await reader.ReadAsync())
            {
                var bug = new Bug
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    ErrorMessage = reader.GetString(2),
                    RootCause = reader.GetString(3),
                    Fix = reader.GetString(4),
                    TimeToSolve = reader.GetInt32(5),
                    StackTags = reader.GetString(6),
                    ResolvedAt = reader.GetDateTime(7),
                    Serverity = GetServerity(reader.GetInt32(8)),
                    CreatedAt = reader.GetDateTime(9),
                };
            }

            if (bugs.Count > 0)
            {
                return bugs;
            }
            else
            {
                return new List<Bug>();
            }
        }
        catch (System.Exception ex)
        {
            Console.WriteLine("Error: ", ex.Message);
            throw;
        }
    }

    public async Task<Bug?> GetBugAsync(int id)
    {
        try
        {
            using var conn = _context.GetConnection();

            if (conn is null)
            {
                throw new Exception("No Connection");
            }
            conn.Open();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM Bugs WHERE Id = $id";

            cmd.Parameters.AddWithValue("$id", id);

            var reader = await cmd.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                var bug = new Bug
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    ErrorMessage = reader.GetString(2),
                    RootCause = reader.GetString(3),
                    Fix = reader.GetString(4),
                    TimeToSolve = reader.GetInt32(5),
                    StackTags = reader.GetString(6),
                    ResolvedAt = reader.GetDateTime(7),
                    Serverity = GetServerity(reader.GetInt32(8)),
                    CreatedAt = reader.GetDateTime(9),
                };

                return bug;
            }

            return null;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine("Error: ", ex.Message);
            throw;
        }
    }

    public async Task<int> CreateBugAsync(Bug bug)
    {
        try
        {
            using var conn = _context.GetConnection();

            if (conn is null)
            {
                throw new Exception("No Connection");
            }
            conn.Open();

            var cmd = conn.CreateCommand();

            cmd.CommandText = """
                INSERT INTO Bugs (Title, ErrorMessage, RootCause, Fix, TimeToSolve, StackTags, ResolvedAt, Serverity, CreatedAt)
                VALUES ($title, $errorMessage, $rootCause, $fix, $timeToSolve, $stackTags, $resolvedAt, $serverity, $createdAt);
                """;

            cmd.Parameters.AddWithValue("$title", bug.Title);
            cmd.Parameters.AddWithValue("$errorMessage", bug.ErrorMessage);
            cmd.Parameters.AddWithValue("$rootCause", bug.RootCause);
            cmd.Parameters.AddWithValue("$fix", bug.Fix);
            cmd.Parameters.AddWithValue("$timeToSolve", bug.TimeToSolve);
            cmd.Parameters.AddWithValue("$stackTags", bug.StackTags);
            cmd.Parameters.AddWithValue("$resolvedAt", bug.ResolvedAt);
            cmd.Parameters.AddWithValue("$serverity", bug.Serverity);
            cmd.Parameters.AddWithValue("$createdAt", bug.CreatedAt);

            return await cmd.ExecuteNonQueryAsync();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine("Error: ", ex.Message);

            throw;
        }
    }

    public async Task<int> UpdateBugAsync(Bug bug)
    {
        try
        {
            using var conn = _context.GetConnection();

            if (conn is null)
            {
                throw new Exception("No Connection");
            }
            conn.Open();

            var cmd = conn.CreateCommand();

            cmd.CommandText = """
                UPDATE Bugs
                SET Title = $title,
                    ErrorMessage = $errorMessage,
                    RootCause = $rootCause,
                    Fix = $fix,
                    TimeToSolve = $timeToSolve,
                    StackTags = $stackTags,
                    ResolvedAt = $resolvedAt,
                    Serverity = $serverity,
                    CreatedAt = $createdAt
                WHERE Id = $id;
                """;

            cmd.Parameters.AddWithValue("$id", bug.Id);
            cmd.Parameters.AddWithValue("$title", bug.Title);
            cmd.Parameters.AddWithValue("$errorMessage", bug.ErrorMessage);
            cmd.Parameters.AddWithValue("$rootCause", bug.RootCause);
            cmd.Parameters.AddWithValue("$fix", bug.Fix);
            cmd.Parameters.AddWithValue("$timeToSolve", bug.TimeToSolve);
            cmd.Parameters.AddWithValue("$stackTags", bug.StackTags);
            cmd.Parameters.AddWithValue("$resolvedAt", bug.ResolvedAt);
            cmd.Parameters.AddWithValue("$serverity", bug.Serverity);
            cmd.Parameters.AddWithValue("$createdAt", bug.CreatedAt);

            return await cmd.ExecuteNonQueryAsync();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine("Error: ", ex.Message);
            throw;
        }
    }

    public async Task<int> DeleteBugAsync(int id)
    {
        try
        {
            using var conn = _context.GetConnection();

            if (conn is null)
            {
                throw new Exception("No Connection");
            }
            conn.Open();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "DELETE FROM Bugs WHERE Id = $id";

            cmd.Parameters.AddWithValue("$id", id);

            return await cmd.ExecuteNonQueryAsync();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine("Error: ", ex.Message);
            throw;
        }
    }

    private Serverity GetServerity(int serverity)
    {
        switch (serverity)
        {
            case 1:
                return Serverity.LOW;
            case 2:
                return Serverity.MEDIUM;
            case 3:
                return Serverity.HIGH;
            default:
                return Serverity.LOW;
        }
    }
}
