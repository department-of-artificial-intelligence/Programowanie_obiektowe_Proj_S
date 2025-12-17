
using Microsoft.EntityFrameworkCore;
using RatingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Text;
namespace RatingSystem.DAL
{
    public class UserDataLogic: IUserDataLogic
    {
        private readonly ApplicationDbContext _context;

        public UserDataLogic(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User?> GetByIdAsync(int id) => await _context.Users.FindAsync(id);

        public async Task<User?> GetByNameAsync(string userName) => await  _context.Users.FirstOrDefaultAsync(u=>u.UserName == userName);

        public async Task AddAsync(User user)=> await _context.Users.AddAsync(user);

        public async Task RemoveAsync(User user)
        {
            _context.Users.Remove(user);
            await Task.CompletedTask;
        }
        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
