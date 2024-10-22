using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Persistence.Repositories;

public class UserRepository(ApplicationDbContext context) : GenericRepository<User>(context), IUserRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> IsUsernameUnique(string username)
    {
        return await _context.Users.AnyAsync(u => u.Username == username) == false;
    }

    public async Task<bool> IsEmailUnique(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email) == false;
    }
}