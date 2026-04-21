using RootCause.Core.Entities;

namespace RootCause.Core.Interfaces;

public interface IBugRepository
{
    public Task<List<Bug>> GetBugsAsync();
    public Task<Bug?> GetBugAsync(int id);
    public Task<int> CreateBugAsync(Bug bug);
    public Task<int> UpdateBugAsync(Bug bug);
    public Task<int> DeleteBugAsync(int id);
}
