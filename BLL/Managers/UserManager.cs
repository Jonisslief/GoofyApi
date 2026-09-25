using BLL.Interfaces;
using DAL.Interfaces;
using Domain.Entities;

namespace BLL.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> GetByNameAsync(string name)
        {
            return await _userRepository.GetByNameAsync(name);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task AddUserAsync(User user)
        {
            await _userRepository.AddAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<Role> GetUserRoleAsync(int id)
        {
            return await _userRepository.GetRoleAsync(id);
        }

        public async Task<Role> GetRoleByNameAsync(string name)
        {
            return await _userRepository.GetRoleByNameAsync(name);
        }

        public async Task<string> GetUserPassword(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user.Password;
        }

        public async Task<bool> UserExists(string username, string email)
        {
            return await _userRepository.UserExists(username, email);
        }

        public async Task UpdateUser(User user)
        {
            await _userRepository.UpdateAsync(user);
        }
    }
}
