using System;
using System.Collections.Generic;
using System.Text;
using RatingSystem.Domain;
namespace RatingSystem.DAL
{
    public interface IRatingDataLogic
    {
        Task<Rating?> GetByIdAsync(int id);
        Task AddAsync(Rating rating);
        Task RemoveAsync(Rating rating);
        Task <IEnumerable<Rating>> GetByServiceIdAsync(int serviceId);
        Task<IEnumerable<Rating>> GetByUserIdAsync(int serviceId);
        Task<IEnumerable<Rating>> GetAllAsync();
        Task<int> SaveChangesAsync();
    }
}
