using APL.Interfaces;
using APL.DTOs;
using BLL.Interfaces;
using Domain.Entities;

namespace APL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserManager _userManager;
        private readonly IPasswordService _passwordService;

        public UserService(IUserManager userManager, IPasswordService passwordService)
        {
            _userManager = userManager;
            _passwordService = passwordService;
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var user = await _userManager.GetByIdAsync(id);
            var role = await _userManager.GetUserRoleAsync(user.RoleId);
            return new UserDTO
            {
                Id = id,
                Name = user.Name,
                Email = user.Email,
                Role = role.Name,
                RoleId = role.Id.ToString()
            };
        }

        public async Task<UserDTO> GetUserByNameAsync(string name)
        {
            var user = await _userManager.GetByNameAsync(name);
            var role = await _userManager.GetUserRoleAsync(user.RoleId);
            return new UserDTO
            {
                Id= user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = role.Name,
                RoleId = role.Id.ToString()
            };
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            var users = await _userManager.GetAllAsync();
            var userDTOs = new List<UserDTO>();
            foreach (var user in users)
            {
                var role = await _userManager.GetUserRoleAsync(user.RoleId);
                userDTOs.Add(new UserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Role = role.Name,
                    RoleId = role.Id.ToString()
                });
            }
            return userDTOs;
        }

        public async Task<bool> AddUserAsync(RegisterUserDTO addUser)
        {
            if (!await _userManager.UserExists(addUser.Name, addUser.Email))
            {
                var user = new User
                {
                    Name = addUser.Name,
                    Email = addUser.Email,
                    Password = _passwordService.HashPassword(addUser.Password)
                };
                await _userManager.AddUserAsync(user);
                return true;
            }
            return false;
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userManager.DeleteUserAsync(id);
        }

        public async Task<string> GetUserRoleAsync(UserDTO userDto)
        {
            var user = await _userManager.GetByIdAsync((int)userDto.Id);
            var role = await _userManager.GetUserRoleAsync(user.RoleId);
            return role.Name;
        }

        public async Task UpdateUser(UserDTO dto)
        {
            var user = await _userManager.GetByIdAsync((int)dto.Id!);
            int roleId;
            if (string.IsNullOrEmpty(dto.RoleId)) { roleId = user.RoleId; }
            else { roleId = int.Parse(dto.RoleId); }

            user.Email = dto.Email;
            user.Name = dto.Name;
            user.RoleId = roleId;

            await _userManager.UpdateUser(user);
        }
    }
}
