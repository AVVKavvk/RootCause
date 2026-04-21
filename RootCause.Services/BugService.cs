using RootCause.Core.Entities;
using RootCause.Core.Interfaces;

namespace RootCause.Services;

public class BugService : IBugService
{
    private readonly IBugRepository _repo;

    public BugService(IBugRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Bug>> GetAllBugsAsync()
    {
        return await _repo.GetBugsAsync();
    }

    public async Task<Bug?> GetBugAsync(int id)
    {
        return await _repo.GetBugAsync(id);
    }

    public async Task<int> CreateBugAsync(Bug bug)
    {
        return await _repo.CreateBugAsync(bug);
    }

    public async Task<int> UpdateBugAsync(Bug bug)
    {
        return await _repo.UpdateBugAsync(bug);
    }

    public async Task<int> DeleteBugAsync(int id)
    {
        return await _repo.DeleteBugAsync(id);
    }
}
