using RatingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RatingSystem.Logic
{
    public  interface Interface1
    {
        Task AddUserAsync(User user);
        Task<User> GetUserByIdAsync(int userId);
    }
}
