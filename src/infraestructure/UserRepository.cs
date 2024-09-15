using Microsoft.EntityFrameworkCore;
using StoreApi.src.domain;
using StoreApi.src.infraestructure.data;

namespace StoreApi.src.infraestructure
{
    public class UserRepository(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users
            .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
            .FirstOrDefaultAsync(p => p.Username == username);
        }

        public async Task<List<User>> GetAllUserAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

    }
}