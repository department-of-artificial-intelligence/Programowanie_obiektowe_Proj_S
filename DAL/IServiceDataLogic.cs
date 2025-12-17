using RatingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public  interface IServiceDataLogic
    {
        Task<Service?> GetByIdAsync(int id);
        Task<IEnumerable<Service>> GetAllAsync();
        Task AddAsync(Service service);
        Task RemoveAsync(Service service);
        Task<int> SaveChangesAsync();
    }
}
