using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using RatingSystem.Domain;
using Microsoft.EntityFrameworkCore;
namespace DAL
{
    public class ServiceDataLogic : IServiceDataLogic
    {
        private readonly ApplicationDbContext _context;
        public ServiceDataLogic(ApplicationDbContext context) { _context = context; }

        public async Task<Service?> GetByIdAsync(int id) => await _context.Services.FindAsync(id);
        public async Task<IEnumerable<Service>> GetAllAsync() => await _context.Services.ToListAsync();
        public async Task AddAsync(Service service)=> await _context.Services.AddAsync(service);
        public async Task RemoveAsync(Service service)
        {
            _context.Services.Remove(service);
            await Task.CompletedTask;
        }
        public async Task<int> SaveChangesAsync()=> await _context.SaveChangesAsync();

    }
}
