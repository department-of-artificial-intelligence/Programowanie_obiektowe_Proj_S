using RatingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public  interface IUserDataLogic
    {
        Task<User?> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task <User?> GetByNameAsync(string name);
        Task SaveChangesAsync();
        Task RemoveAsync(User user);
    }
}
