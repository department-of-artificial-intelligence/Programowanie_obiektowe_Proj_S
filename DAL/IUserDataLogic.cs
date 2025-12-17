using RatingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RatingSystem.DAL
{
    public  interface IUserDataLogic
    {
        Task<User?> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task <User?> GetByNameAsync(string name);
        Task<int>SaveChangesAsync();
        Task RemoveAsync(User user);
    }
}
