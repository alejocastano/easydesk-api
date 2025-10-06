using EasyDesk.Domain;

namespace EasyDesk.Application;

public interface IUserService
{
    Task<User> GetUserByIdAsync(int id);
} 