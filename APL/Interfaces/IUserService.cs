using APL.DTOs;

namespace APL.Interfaces
{
    public interface IUserService
    {
        Task<bool> AddUserAsync(RegisterUserDTO addUser);
        Task DeleteUserAsync(int id);
        Task<List<UserDTO>> GetAllAsync();
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<UserDTO> GetUserByNameAsync(string name);
        Task<string> GetUserRoleAsync(UserDTO userDto);
        Task UpdateUser(UserDTO user);
    }
}
