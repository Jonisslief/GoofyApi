using Domain.Entities;

namespace BLL.Interfaces
{
    public interface IUserManager
    {
        Task AddUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> GetByNameAsync(string name);
        Task<Role> GetRoleByNameAsync(string name);
        Task<string> GetUserPassword(int id);
        Task<Role> GetUserRoleAsync(int id);
        Task UpdateUser(User user);
        Task<bool> UserExists(string username, string email);
    }
}
