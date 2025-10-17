using EasyDesk.Domain;

namespace EasyDesk.Application;

public class RoleService : IRoleService
{
    public Task AssignRoleToUserAsync(int userId, RoleType role)
    {
        // Implementation to assign role to user
        throw new NotImplementedException();
    }

    public Task RemoveRoleFromUserAsync(int userId, RoleType role)
    {
        // Implementation to remove role from user
        throw new NotImplementedException();
    }
}