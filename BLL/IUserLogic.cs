using RatingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IUserLogic
    {
        Task<User> RegisterUserAsync(string name);
        Task<User?> GetUserByIdAsync(int id);
        Task DeleteUserAsync(int UserId);

    }
}
