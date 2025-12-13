using System;
using System.Collections.Generic;
using System.Text;
using RatingSystem.Domain;
using RatingSystem;
namespace RatingSystem.DAL
{
    public interface IServiceRepository
    { 
        Task AddAsync(Service service); 
        Task<Service> GetByIdAsync(int id);   
        Task<IEnumerable<Service>> GetAllAsync();

        
    }
}
