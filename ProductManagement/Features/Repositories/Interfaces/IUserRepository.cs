using ProductManagement.Features.Data.Models;

namespace ProductManagement.Features.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetByRoleAsync(UserRole role);
        Task<IEnumerable<User>> GetActiveUsersAsync();
        Task<User?> GetByResetTokenAsync(string token);
        Task<bool> ExistsAsync(string email);
    }
}
