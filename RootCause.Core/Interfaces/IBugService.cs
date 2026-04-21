using RootCause.Core.Entities;

namespace RootCause.Core.Interfaces;

public interface IBugService
{
    Task<IEnumerable<Bug>> GetAllBugsAsync();
    Task<Bug> GetBugAsync(int id);
    Task<Bug> CreateBugAsync(Bug bug);
    Task<Bug> UpdateBugAsync(Bug bug);
    Task<bool> DeleteBugAsync(int id);
}
