using EasyDesk.Domain;

namespace EasyDesk.Infrastructure;

public interface IUserRepository
{
    Task<User> GetByIdAsync(int id);
}