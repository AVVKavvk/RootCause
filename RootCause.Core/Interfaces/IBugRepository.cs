using RootCause.Core.Entities;

namespace RootCause.Core.Interfaces;

public interface IBugRepository
{
    public Task<List<Bug>> GetBugsAsync();
    public Task<Bug> GetBugAsync(int id);
    public Task<Bug> CreateBugAsync(Bug bug);
    public Task<Bug> UpdateBugAsync(Bug bug);
    public Task<Bug> DeleteBugAsync(int id);
}
