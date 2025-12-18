using AutoMapper;
using Project.DTO;
using Microsoft.EntityFrameworkCore;
using Project.Domain;
using Project.Model;

namespace Project.Services;

public class UserService : BaseService<User, int, UserDto>, IUserService
{
    public UserService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
    
    // public async Task<UserDto?> CreateByEntityAsync(User user)
    // {
    //     user.CreatedAt = DateTime.UtcNow;
    //     user.UpdatedAt = DateTime.UtcNow;
    //     
    //     await _dbSet.AddAsync(user);
    //     await _context.SaveChangesAsync();
    //     
    //     return _mapper.Map<UserDto>(user);
    // }

    public async Task<UserDto?> GetByUsernameAsync(string username)
    {
        var user = await _dbSet
            .FirstOrDefaultAsync(u => u.Username == username);
        
        return user == null ? null : _mapper.Map<UserDto>(user);
    }

    public async Task<bool> UsernameExistsAsync(string username)
    {
        return await _dbSet.AnyAsync(u => u.Username == username);
    }
}

