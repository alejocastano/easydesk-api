using EasyDesk.Domain;
using EasyDesk.Infrastructure;

namespace EasyDesk.Application;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<User> GetUserByIdAsync(int id)
    {
        return _userRepository.GetByIdAsync(id);
    }
}