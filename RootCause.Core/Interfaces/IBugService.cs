using RootCause.Core.Entities;

namespace RootCause.Core.Interfaces;

public interface IBugService
{
    Task<List<Bug>> GetAllBugsAsync();
    Task<Bug?> GetBugAsync(int id);
    Task<int> CreateBugAsync(Bug bug);
    Task<int> UpdateBugAsync(Bug bug);
    Task<int> DeleteBugAsync(int id);
}
