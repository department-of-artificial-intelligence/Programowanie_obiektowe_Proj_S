using Project.DTO;
using Project.Model;

namespace Project.Services;

public interface IUserService : IBaseService<User, int, UserDto>
{
    Task<UserDto?> GetByUsernameAsync(string username);
    Task<bool> UsernameExistsAsync(string username);
    
    // Task<UserDto?> CreateByEntityAsync(User user);
}

