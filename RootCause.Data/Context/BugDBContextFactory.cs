using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RootCause.Data.Helpers;

namespace RootCause.Data.Context;

public class BugDBContextFactory : IDesignTimeDbContextFactory<BugDBContext>
{
    public BugDBContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BugDBContext>();

        var dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            "RootCauseData.db3"
        );

        optionsBuilder.UseSqlite($"Data Source={dbPath}");

        return new BugDBContext(optionsBuilder.Options);
    }
}
