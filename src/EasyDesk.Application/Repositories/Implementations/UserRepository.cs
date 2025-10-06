using EasyDesk.Domain;
using EasyDesk.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EasyDesk.Application;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}