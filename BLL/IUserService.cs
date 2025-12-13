using RatingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using RatingSystem.DAL;
namespace RatingSystem.BLL { 
    public  interface IUserService
    {
        Task AddUserAsync(User user);
        Task<User> GetUserByIdAsync(int userId);
    }
}
