using EasyDesk.Domain;

namespace EasyDesk.Application;

public interface IRoleRepository
{
    Task AssignRoleToUserAsync(int userId, RoleType role);
    Task RemoveRoleFromUserAsync(int userId, RoleType role);
}