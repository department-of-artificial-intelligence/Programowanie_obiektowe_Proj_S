using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using RatingSystem.Domain;
namespace RatingSystem.DAL
{
    public class RatingDataLogic : IRatingDataLogic
    {
        private readonly ApplicationDbContext _context;

       

        
        public RatingDataLogic(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Rating?> GetByIdAsync(int id) => await _context.Ratings.FindAsync(id);
        public async Task<IEnumerable<Rating?>> GetByServiceIdAsync(int serviceId) => await _context.Ratings.Where(r => r.ServiceId == serviceId).ToListAsync();
        public async Task<IEnumerable<Rating>> GetAllAsync()=>
          await _context.Ratings
                .Include(r => r.Service) 
                .Include(r => r.User)    
                .ToListAsync();
       
        public async Task<IEnumerable<Rating?>> GetByUserIdAsync(int userId) => await _context.Ratings.Where(r => r.UserId == userId).ToListAsync();
        public async Task AddAsync(Rating rating) => await _context.AddAsync(rating);
        public async Task RemoveAsync(Rating rating)
        {
            _context.Remove(rating);
            await Task.CompletedTask;
        }
        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    }
}
