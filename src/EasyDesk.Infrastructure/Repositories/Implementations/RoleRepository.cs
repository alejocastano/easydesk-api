using EasyDesk.Domain;
using EasyDesk.Application;
using Microsoft.EntityFrameworkCore;

namespace EasyDesk.Infrastructure;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _context;

    public RoleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AssignRoleToUserAsync(int userId, RoleType role)
    {
        _context.UserRoles.Add(new UserRole
        {
            UserId = userId,
            RoleId = (int)role
        });

        await _context.SaveChangesAsync();
    }

    public async Task RemoveRoleFromUserAsync(int userId, RoleType role)
    {
        var ur = await _context.UserRoles
            .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == (int)role);
        _context.UserRoles.Remove(ur);
        await _context.SaveChangesAsync();
    }
}