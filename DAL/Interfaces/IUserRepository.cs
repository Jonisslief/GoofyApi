using Domain.Entities;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task DeleteAsync(int id);
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> GetByNameAsync(string name);
        Task<Role> GetRoleAsync(int id);
        Task<Role> GetRoleByNameAsync(string name);
        Task UpdateAsync(User user);
        Task<bool> UserExists(string username, string email);
    }
}
