using EasyDesk.Domain;

namespace EasyDesk.Application;

public interface IUserRepository
{
    Task<User> GetByIdAsync(int id);
}