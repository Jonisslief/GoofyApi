using DAL.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        //Needs to catch Null error 
        public async Task<User> GetByNameAsync(string name)
        {
            return await _context.Users.FirstAsync(x => x.Name == name);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Role> GetRoleAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<Role> GetRoleByNameAsync(string name)
        {
            return await _context.Roles.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<bool> UserExists(string username, string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == username || x.Email == email);
            if(user != null)
            {
                return true;
            }
            return false;
        }
    }
}
