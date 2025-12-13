using RatingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RatingSystem.Logic
{
    public  interface IUserService
    {
        Task AddUserAsync(User user);
        Task<User> GetUserByIdAsync(int userId);
    }
}
