
using SportSupplements_API.Core.Models;

namespace SportSupplements_API.Core.Repositories;

public interface ISportSupplementRepository
{
    Task<IEnumerable<SportSupplement>> GetAllAsync();
    Task CreateAsync(SportSupplement supplement);
    Task UpdateAsync(int id, SportSupplement updatedSupplement);
    Task DeleteAsync(int id);
    Task<SportSupplement> GetByIdAsync(int id);
    Task ApproveAsync(int id);
}
