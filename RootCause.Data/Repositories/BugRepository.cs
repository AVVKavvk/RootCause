using Microsoft.EntityFrameworkCore;
using RootCause.Core.Entities;
using RootCause.Core.Interfaces;
using RootCause.Data.Context;

namespace RootCause.Data.Repositories;

public class BugRepository : IBugRepository
{
    private readonly BugDBContext _context;

    public BugRepository(BugDBContext context)
    {
        _context = context;
    }

    public async Task<List<Bug>> GetBugsAsync()
    {
        return await _context.Bugs.ToListAsync();
    }

    public async Task<Bug?> GetBugAsync(int id)
    {
        return await _context.Bugs.FindAsync(id);
    }

    public async Task<int> CreateBugAsync(Bug bug)
    {
        _context.Bugs.Add(bug);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateBugAsync(Bug bug)
    {
        _context.Bugs.Update(bug);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> DeleteBugAsync(int id)
    {
        var bug = await _context.Bugs.FindAsync(id);

        if (bug is null)
            return 0;

        _context.Bugs.Remove(bug);
        return await _context.SaveChangesAsync();
    }
}
