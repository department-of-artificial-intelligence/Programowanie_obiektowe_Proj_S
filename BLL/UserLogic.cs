using DAL;
using System;
using System.Collections.Generic;
using System.Text;
using RatingSystem.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Runtime.CompilerServices;
namespace BLL
{
    public class UserLogic:IUserLogic
    {
        private readonly IUserDataLogic _userDataLogic;
        public UserLogic(IUserDataLogic userDataLogic)
        {
            _userDataLogic = userDataLogic;
        }
        public async Task<User> RegisterUserAsync(string userName)
        {
            if(string.IsNullOrWhiteSpace(userName) || userName.Length<3)
            {
                throw new ArgumentException("user name should contain more than 3 chars, cannot be a null or white space");
            }
            User? user = await _userDataLogic.GetByNameAsync(userName);
            if (user != null)
            {
                throw new Exception($"user -  {userName} already exists");
            }
            var newUser = new User
            {
                UserName = userName.Trim()
            };
            await _userDataLogic.AddAsync(newUser);
            await _userDataLogic.SaveChangesAsync();
            return newUser;
        }
        public async Task<User?> GetUserByIdAsync(int userId)
        {
           User? user =await  _userDataLogic.GetByIdAsync(userId);
            return user;
        }
        public async Task DeleteUserAsync(int userId)
        {
            User? user = await _userDataLogic.GetByIdAsync(userId);
            if (user != null) {
                await _userDataLogic.RemoveAsync(user);
                await _userDataLogic.SaveChangesAsync();
            }

        }

    }
}
